using System.Net.Http.Headers;
using System.Text.Json;
using SkolplattformenElevApi.Models;
using SkolplattformenElevApi.Models.Internal.Timetable;

namespace SkolplattformenElevApi;

public partial class Api
{
    private async Task TimetableSsoLoginAsync()
    {
        var temp_url =
            "https://fnsservicesso1.stockholm.se/sso-ng/saml-2.0/authenticate?customer=https://login001.stockholm.se&targetsystem=TimetableViewer";

        var temp_res = await _httpClient.GetAsync(temp_url);
        var temp_content = await temp_res.Content.ReadAsStringAsync();
        if (temp_res.Headers.Location != null)
        {
            temp_content = await TimetableSsoProvideCredentialsAsync(temp_res.Headers.Location);
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
            temp_url = temp_url.StartsWith("/") ? "https://fns.stockholm.se" + temp_url : temp_url;
            temp_res = await _httpClient.GetAsync(temp_url);
        }
    }

    private async Task<string> TimetableSsoProvideCredentialsAsync(Uri uri)
    {
        var smagentname = RegExp("SMAGENTNAME=([^&]*)", uri.ToString());

        var temp_res = await _httpClient.GetAsync(uri);

        // switch to student login
        var url = "https://login001.stockholm.se/siteminderagent/forms/aelever.jsp?SMAUTHREASON=0"
                  + $"&SMAGENTNAME={smagentname}"
                  + "&SMQUERYDATA=null"
                  + "&TARGET=-SM-HTTPS://fnsservicesso1.stockholm.se/sso-ng/saml-2.0/authenticate?customer=https://login001.stockholm.se&targetsystem=TimetalbeViewer";

        temp_res = await _httpClient.GetAsync(url);

        // load login page
        url =
            "https://login001.stockholm.se/siteminderagent/forms/loginForm.jsp" +
            "?SMAGENTNAME=login001-ext.stockholm.se" +
            "&POSTTARGET=https://login001.stockholm.se/NECSelev/form/b64startpage.jsp?startpage=aHR0cHM6Ly9mbnNzZXJ2aWNlc3NvMS5zdG9ja2hvbG0uc2Uvc3NvLW5nL3NhbWwtMi4wL2F1dGhlbnRpY2F0ZT9jdXN0b21lcj1odHRwczovL2xvZ2luMDAxLnN0b2NraG9sbS5zZSZ0YXJnZXRzeXN0ZW09U2tvbGEyNFdpZGdldA==" +
            "&TARGET=-SM-HTTPS://fnsservicesso1.stockholm.se/sso-ng/saml-2.0/authenticate?customer=https://login001.stockholm.se";
        temp_res = await _httpClient.GetAsync(url);

        var temp_url = "https://login001.stockholm.se/siteminderagent/forms/login.fcc";
        temp_res = await _httpClient.PostAsync(temp_url, new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("user", _username),
            new KeyValuePair<string, string>("password", _password),
            new KeyValuePair<string, string>("SMENC", "ISO-8859-1"),
            new KeyValuePair<string, string>("SMLOCALE", "US-EN"),
            new KeyValuePair<string, string>("target", "https://login001.stockholm.se/NECSelev/form/b64startpage.jsp?startpage=aHR0cHM6Ly9mbnNzZXJ2aWNlc3NvMS5zdG9ja2hvbG0uc2Uvc3NvLW5nL3NhbWwtMi4wL2F1dGhlbnRpY2F0ZT9jdXN0b21lcj1odHRwczovL2xvZ2luMDAxLnN0b2NraG9sbS5zZSZ0YXJnZXRzeXN0ZW09U2tvbGEyNFdpZGdldA=="),
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
    

    private async Task<(string, string)> GetTimetableUnitGuidAndPersonGuid()
    {
        var temp_url = "https://fns.stockholm.se/ng/api/services/skola24/get/personal/timetables";

        var content = "{\"getPersonalTimetablesRequest\":{\"hostName\":\"fns.stockholm.se\"}}";
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(temp_url),
            Method = HttpMethod.Post,
            Headers =
            {
                { "Referer", "https://fns.stockholm.se/ng/timetable/timetable-viewer" },
                { "X-Scope", "8a22163c-8662-4535-9050-bc5e1923df48" },
                { "Accept", "application/json" },
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

        var obj = JsonSerializer.Deserialize<TimetablePersonalTimetablesResponse>(temp_content, jsonSerializerOptions);

        var unitGuid = obj.Data.GetPersonalTimetablesResponse.StudentTimetables.FirstOrDefault().UnitGuid;
        var personGuid = obj.Data.GetPersonalTimetablesResponse.StudentTimetables.FirstOrDefault().PersonGuid;

        return (unitGuid, personGuid);
    }

    private async Task<string> GetTimetableRenderKey()
    {
        var temp_url = "https://fns.stockholm.se/ng/api/get/timetable/render/key";

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(temp_url),
            Method = HttpMethod.Post,
            Headers =
            {
                { "Referer", "https://fns.stockholm.se/ng/timetable/timetable-viewer/fns.stockholm.se/" },
                { "X-Scope", "8a22163c-8662-4535-9050-bc5e1923df48" },
                { "Accept", "application/json" },
                { "Origin", "https://fns.stockholm.se" },
            },
        };

        var temp_res = await _httpClient.SendAsync(request);
        var temp_content = await temp_res.Content.ReadAsStringAsync();

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var obj = JsonSerializer.Deserialize<TimetableRenderKeyResponse>(temp_content, jsonSerializerOptions);
        var key = obj.Data.Key;

        return key;
    }

    public async Task<List<TimeTableLesson>> GetTimetableAsync(int year, int week)
    {
        var part = ApiPart.GetTimetable;

        try
        {
            var (unitGuid, personGuid) = await GetTimetableUnitGuidAndPersonGuid();
            var key = await GetTimetableRenderKey();

            var content = "{\"renderKey\":\"" + key + "\",\"host\":\"fns.stockholm.se\",\"unitGuid\":\"" + unitGuid +
                          "\",\"startDate\":null,\"endDate\":null,\"scheduleDay\":0,\"blackAndWhite\":false,\"width\":1227,\"height\":1191,\"selectionType\":5,\"selection\":\"" +
                          personGuid + "\",\"showHeader\":false,\"periodText\":\"\",\"week\":" + week + ",\"year\":" +
                          year + ",\"privateFreeTextMode\":null,\"privateSelectionMode\":true,\"customerKey\":\"\"}";

            var temp_url = "https://fns.stockholm.se/ng/api/render/timetable";

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(temp_url),
                Method = HttpMethod.Post,
                Headers =
            {
                { "Referer", "https://fns.stockholm.se/ng/timetable/timetable-viewer/fns.stockholm.se/" },
                { "X-Scope", "8a22163c-8662-4535-9050-bc5e1923df48" },
                { "Accept", "application/json" },
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

            var authorizeResponse =
                JsonSerializer.Deserialize<TimetableRenderResponse>(temp_content, jsonSerializerOptions);

            if (authorizeResponse?.Data?.LessonInfo == null)
            {
                return new List<TimeTableLesson>();
            }

            var lessonList = new List<TimeTableLesson>();
            foreach (var item in authorizeResponse.Data.LessonInfo)
            {
                lessonList.Add(new TimeTableLesson
                {
                    DayOfWeekNumber = item.DayOfWeekNumber,
                    TimeStart = item.TimeStart,
                    TimeEnd = item.TimeEnd,
                    SubjectCode = item.Texts[0],
                    SubjectName = item.Texts[0],
                    TeacherCode = item.Texts[1],
                    TeacherName = item.Texts[1],
                    Location = item.Texts[2]
                });
            }

            UpdateStatus(part, lessonList.Count > 0 ? ApiReadSuccessIndicator.Success: ApiReadSuccessIndicator.NoData);

            return lessonList;
        }
        catch
        {
            UpdateStatus(part,ApiReadSuccessIndicator.Error);
            return new List<TimeTableLesson>();
        }

        
    }

    public void EnrichTimetableWithTeachers(List<TimeTableLesson> timetable, List<Teacher> teachers)
    {
        Utils.Enrichers.EnrichTimetableWithTeachers(timetable, teachers);
    }

    public void EnrichTimetableWithCurriculum(List<TimeTableLesson> timetable)
    {
        Utils.Enrichers.EnrichTimetableWithCurriculum(timetable);
    }
}
