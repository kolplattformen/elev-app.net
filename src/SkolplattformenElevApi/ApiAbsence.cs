using System.Net.Http.Headers;
using System.Text.Json;
using SkolplattformenElevApi.Models;
using SkolplattformenElevApi.Models.Internal.Absence;

namespace SkolplattformenElevApi;

public partial class Api
{
    private async Task AbsenceSsoLoginAsync()
    {
        var temp_url = "https://fnsservicesso1.stockholm.se/sso-ng/saml-2.0/authenticate?customer=https://login001.stockholm.se&targetsystem=Skola24Widget";
        
        var temp_res = await _httpClient.GetAsync(temp_url);
        var temp_content = await temp_res.Content.ReadAsStringAsync();

        var samlRequest = RegExp("\"SAMLRequest\\\" value=\\\"([^\\\"]*)\"", temp_content);
        temp_url = RegExp("action=\\\"([^\\\"]*)", temp_content);

        temp_res = await _httpClient.PostAsync(temp_url, new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("SAMLRequest", samlRequest),
        }));

        temp_content = await temp_res.Content.ReadAsStringAsync();
        var samlResponse = RegExp("\"SAMLResponse\\\" value=\\\"([^\\\"]*)\"", temp_content);
        temp_url = RegExp("action=\\\"([^\\\"]*)", temp_content);

        temp_res = await _httpClient.PostAsync(temp_url, new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("SAMLResponse", samlResponse),
        }));

        while (temp_res.Headers.Location != null)
        {
            temp_url = temp_res.Headers.Location?.ToString();
            temp_url = temp_url.StartsWith("/") ? "https://fns.stockholm.se" + temp_url : temp_url;
            temp_res = await _httpClient.GetAsync(temp_url);
        }
    }

    private async Task GetAbsenceUserInfo()
    {
        var temp_url = "https://fns.stockholm.se/ng/api/get/user/info";

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(temp_url),
            Method = HttpMethod.Post,
            Headers =
            {
                { "Referer", "https://fns.stockholm.se/ng/portal/start" },
                { "X-Scope", "a0b6c9c4-11d7-4a52-a030-a55a15058eef" },
                { "Accept", "application/json"},
                { "Origin", "https://fns.stockholm.se" },
            },
        };

        var temp_res = await _httpClient.SendAsync(request);
        var temp_content = await temp_res.Content.ReadAsStringAsync();

    }

    private async Task<string> GetPlannedAbsenceUserGuid()
    {
        var temp_url = "https://fns.stockholm.se/ng/api/get/guid/for/current/user";

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(temp_url),
            Method = HttpMethod.Post,
            Headers =
            {
                { "Referer", "https://fns.stockholm.se/ng/portal/start/absence/planned" },
                { "X-Scope", "f9193d2f-b9f5-41a5-b5ca-b2f52690b27e" },
                { "Accept", "application/json"},
                { "Origin", "https://fns.stockholm.se" },
            },
        };

        var temp_res = await _httpClient.SendAsync(request);
        var temp_content = await temp_res.Content.ReadAsStringAsync();

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var obj = JsonSerializer.Deserialize<CurrentUserGuidResponse>(temp_content, jsonSerializerOptions);
        var guid = obj.Data.CurrentUserGuid;
        
        return guid;
    }

    public async Task<List<PlannedAbsenceItem>> GetPlannedAbsenceListAsync()
    {
        await GetAbsenceUserInfo();
        var guid = await GetPlannedAbsenceUserGuid();

        var content = "{\"studentPersonGuid\":\"" + guid + "\",\"groupGuid\":null,\"isPrivate\":true,\"selectedDate\":null}";

        var temp_url = "https://fns.stockholm.se/ng/api/get/planned/absence";

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(temp_url),
            Method = HttpMethod.Post,
            Headers =
            {
                { "Referer", "https://fns.stockholm.se/ng/portal/start/absence/planned" },
                { "X-Scope", "f9193d2f-b9f5-41a5-b5ca-b2f52690b27e" },
                { "Accept", "application/json"},
                { "Origin", "https://fns.stockholm.se" },
            },
            Content = new StringContent(content)
        };
        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

        var temp_res = await _httpClient.SendAsync(request);

        var temp_content = await temp_res.Content.ReadAsStringAsync();

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var authorizeResponse = JsonSerializer.Deserialize<PlannedAbsenceResponse>(temp_content, jsonSerializerOptions);

        if (authorizeResponse?.Data?.PlannedAbsences == null)
        {
            return new List<PlannedAbsenceItem>();
        }

        var absenceList = new List<PlannedAbsenceItem>();
        foreach (var pa in authorizeResponse.Data.PlannedAbsences)
        {
            absenceList.Add(new PlannedAbsenceItem
            {
                Created = pa.AbsenceCreationTime,
                AbsenceId = pa.AbsenceId!,
                Comment = pa.Comment ?? string.Empty,
                DateTimeFrom = pa.DateTimeFrom,
                DateTimeTo = pa.DateTimeTo,
                IsFullDayAbsence = pa.IsFullDayAbsence,
                ReasonDescription = pa.ReasonDescription ?? string.Empty,
                Reporter = pa.Reporter ?? string.Empty

            });
        }

        return absenceList;
    }
}

