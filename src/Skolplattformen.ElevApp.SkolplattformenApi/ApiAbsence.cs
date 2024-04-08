using System.Net.Http.Headers;
using System.Text.Json;
using Skolplattformen.ElevApp.ApiInterface;
using Skolplattformen.ElevApp.ApiInterface.Models;
using Skolplattformen.ElevApp.SkolplattformenApi.Models.Internal.Absence;

namespace Skolplattformen.ElevApp.SkolplattformenApi;

public partial class Api
{
    private async Task AbsenceSsoLoginAsync()
    {
        var temp_url = "https://stockholm-sso.skola24.se/nssso/saml-2.0/authenticate?customer=https://login001.stockholm.se&targetsystem=Skola24Widget";


        var temp_res = await _httpClient.GetAsync(temp_url);
        var temp_content = await temp_res.Content.ReadAsStringAsync();

        if (temp_res.Headers.Location != null)
        {
            temp_content = await AbsenceSsoProvideCredentialsAsync(temp_res.Headers.Location);
        }

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
            temp_url = temp_url.StartsWith("/") ? "https://websthlm.skola24.se" + temp_url : temp_url;
            temp_res = await _httpClient.GetAsync(temp_url);
        }
    }

    private async Task<string> AbsenceSsoProvideCredentialsAsync(Uri uri)
    {
        var smagentname = RegExp("SMAGENTNAME=([^&]*)", uri.ToString());

        var temp_res = await _httpClient.GetAsync(uri);

        // switch to student login
        var url = "https://login001.stockholm.se/siteminderagent/forms/aelever.jsp?SMAUTHREASON=0"
                  + $"&SMAGENTNAME={smagentname}"
                  + "&SMQUERYDATA=null"
                  + "&TARGET=-SM-HTTPS://stockholm-sso.skola24.se/nssso/saml-2.0/authenticate?customer=https://login001.stockholm.se&targetsystem=Skola24Widget";

        temp_res = await _httpClient.GetAsync(url);
        //var temp_content = await temp_res.Content.ReadAsStringAsync();

        // load login page
        url =
            "https://login001.stockholm.se/siteminderagent/forms/loginForm.jsp"+
            "?SMAGENTNAME=login001-ext.stockholm.se"+
            "&POSTTARGET=https://login001.stockholm.se/NECSelev/form/b64startpage.jsp?startpage=aHR0cHM6Ly9zdG9ja2hvbG0tc3NvLnNrb2xhMjQuc2UvbnNzc28vc2FtbC0yLjAvYXV0aGVudGljYXRlP2N1c3RvbWVyPWh0dHBzOi8vbG9naW4wMDEuc3RvY2tob2xtLnNlJnRhcmdldHN5c3RlbT1Ta29sYTI0V2lkZ2V0" +
            "&TARGET=-SM-HTTPS://stockholm-sso.skola24.se/nssso/saml-2.0/authenticate?customer=https://login001.stockholm.se";
        temp_res = await _httpClient.GetAsync(url);
        //temp_content = await temp_res.Content.ReadAsStringAsync();

        var temp_url = "https://login001.stockholm.se/siteminderagent/forms/login.fcc";
        temp_res = await _httpClient.PostAsync(temp_url, new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("user", _username),
            new KeyValuePair<string, string>("password", _password),
            new KeyValuePair<string, string>("SMENC", "ISO-8859-1"),
            new KeyValuePair<string, string>("SMLOCALE", "US-EN"),
            new KeyValuePair<string, string>("target", "https://login001.stockholm.se/NECSelev/form/b64startpage.jsp?startpage=aHR0cHM6Ly9zdG9ja2hvbG0tc3NvLnNrb2xhMjQuc2UvbnNzc28vc2FtbC0yLjAvYXV0aGVudGljYXRlP2N1c3RvbWVyPWh0dHBzOi8vbG9naW4wMDEuc3RvY2tob2xtLnNlJnRhcmdldHN5c3RlbT1Ta29sYTI0V2lkZ2V0"),
            new KeyValuePair<string, string>("smauthreason", "null"),
            new KeyValuePair<string, string>("smagentname", "login001-ext.stockholm.se"),
            new KeyValuePair<string, string>("smquerydata", "null"),
            new KeyValuePair<string, string>("postpreservationdata", "null"),
            new KeyValuePair<string, string>("submit", "")
        }));

        while (temp_res.Headers.Location != null)
        {
            temp_url = temp_res.Headers.Location?.ToString();
            temp_url = temp_url.StartsWith("/") ? "https://login001.stockholm.se" + temp_url : temp_url;
            temp_res = await _httpClient.GetAsync(temp_url);
        }
        var content = await temp_res.Content.ReadAsStringAsync();

        return content;
    }

    private async Task GetAbsenceUserInfo()
    {
        var temp_url = "https://websthlm.skola24.se/api/get/user/info";

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(temp_url),
            Method = HttpMethod.Post,
            Headers =
            {
                { "Referer", "https://websthlm.skola24.se/portal/start" },
                { "X-Scope", "a0b6c9c4-11d7-4a52-a030-a55a15058eef" },
                { "Accept", "application/json"},
                { "Origin", "https://websthlm.skola24.se" },
            },
        };

        var temp_res = await _httpClient.SendAsync(request);
        var temp_content = await temp_res.Content.ReadAsStringAsync();

    }

    private async Task<string> GetPlannedAbsenceUserGuid()
    {
        var temp_url = "https://websthlm.skola24.se/api/get/guid/for/current/user";

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(temp_url),
            Method = HttpMethod.Post,
            Headers =
            {
                { "Referer", "https://websthlm.skola24.se/portal/start/absence/planned" },
                { "X-Scope", "f9193d2f-b9f5-41a5-b5ca-b2f52690b27e" },
                { "Accept", "application/json"},
                { "Origin", "https://websthlm.skola24.se" },
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
        var part = ApiPart.GetPlannedAbsence;

        try
        {
            await GetAbsenceUserInfo();
            var guid = await GetPlannedAbsenceUserGuid();

            var content = "{\"studentPersonGuid\":\"" + guid +
                          "\",\"groupGuid\":null,\"isPrivate\":true,\"selectedDate\":null}";

            var temp_url = "https://websthlm.skola24.se/api/get/planned/absence";

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(temp_url),
                Method = HttpMethod.Post,
                Headers =
                {
                    { "Referer", "https://websthlm.skola24.se/portal/start/absence/planned" },
                    { "X-Scope", "f9193d2f-b9f5-41a5-b5ca-b2f52690b27e" },
                    { "Accept", "application/json" },
                    { "Origin", "https://websthlm.skola24.se" },
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

            var authorizeResponse =
                JsonSerializer.Deserialize<PlannedAbsenceResponse>(temp_content, jsonSerializerOptions);

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

            UpdateStatus(part,
                absenceList.Count > 0 ? ApiReadSuccessIndicator.Success : ApiReadSuccessIndicator.NoData);

            return absenceList;

        }
        catch
        {
            UpdateStatus(part, ApiReadSuccessIndicator.Error);
            return new List<PlannedAbsenceItem>();
        }
    }
}

