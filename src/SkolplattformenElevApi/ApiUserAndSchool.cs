using System.Net.Http.Headers;
using System.Text.Json;
using System.Web;
using SkolplattformenElevApi.Models;
using SkolplattformenElevApi.Models.Internal.StockholmAzureApi;

namespace SkolplattformenElevApi;

public partial class Api
{

    public async Task<ApiUser?> GetUserAsync()
    {

        var token = await GetAzureApiAccessTokenAsync();

        var url = "https://stockholm-o365-api.azurewebsites.net/api/User/getUser";

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(url),
            Method = HttpMethod.Get,
            Headers =
            {
                { "Referer", "https://elevstockholm.sharepoint.com/" },
                { "Accept", "application/json"},
                { "Origin", "https://elevstockholm.sharepoint.com" },
            },
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.SendAsync(request);



        var content = await response.Content.ReadAsStringAsync();

        var deserialized = JsonSerializer.Deserialize<GetUserResponse>(content);

        if (deserialized?.Data == null)
        {
            return null;
        }

        var u = deserialized.Data;
        var user = new ApiUser
        {
            Id = Guid.Parse(u.Id),
            ExternalId = Guid.Parse(u.ExternalId),
            Name = u.Name,
            PrimarySchoolGuid = Guid.Parse(u.PrimarySchool.Split(';').First()),
            PrimarySchoolName = u.PrimarySchool.Split(';').Last(),
        };
        foreach (var s in u.Schools)
        {
            user.Schools.Add(new School
            {
                ExternalId = Guid.Parse(s.ExternalId),
                Name = s.DisplayName,
                Id = Guid.Parse(s.Id),
                Url = s.Url
            });
        }

        return user;
    }


    public async Task<List<Teacher>> GetTeachersAsync()
    {

        var token = await GetAzureApiAccessTokenAsync();

        var url = "https://stockholm-o365-api.azurewebsites.net//api/SDS/student/teacher";

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(url),
            Method = HttpMethod.Get,
            Headers =
            {
                { "Referer", "https://elevstockholm.sharepoint.com/" },
                { "Accept", "application/json"},
                { "Origin", "https://elevstockholm.sharepoint.com" },
            },
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.SendAsync(request);



        var content = await response.Content.ReadAsStringAsync();

        var deserialized = JsonSerializer.Deserialize<GetTeachersResponse>(content);

        if (deserialized?.Data == null)
        {
            return new List<Teacher>();
        }

        var teacherList = new List<Teacher>();
        foreach (var item in deserialized.Data)
        {
            if (teacherList.All(t => t.Id != item.ID))
            {
                teacherList.Add(new Teacher
                {
                    Id = item.ID,
                    Firstname = item.FIRSTNAME,
                    Lastname = item.LASTNAME,
                    Email = item.EMAILADDRESS
                });
            }
        }

        return teacherList;
    }

    public void EnrichTeachersWithSubjects(List<Teacher> teachers, List<TimeTableLesson> timetable)
    {
        Utils.Enrichers.EnrichTeachersWithSubjects(teachers, timetable);
    }

    public async Task<SchoolDetails?> GetSchoolDetailsAsync(Guid schoolId)
    {

        var token = await GetAzureApiAccessTokenAsync();

        var url = $"https://stockholm-o365-api.azurewebsites.net//api/soa/school/{schoolId.ToString().ToUpper()}";

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(url),
            Method = HttpMethod.Get,
            Headers =
            {
                { "Referer", "https://elevstockholm.sharepoint.com/" },
                { "Accept", "application/json"},
                { "Origin", "https://elevstockholm.sharepoint.com" },
            },
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.SendAsync(request);



        var content = await response.Content.ReadAsStringAsync();


        var deserialized = JsonSerializer.Deserialize<GetSchoolResponse>(content);

        if (deserialized?.Data == null)
        {
            return null;
        }

        var s = deserialized.Data;
        var schoolDetails = new SchoolDetails
        {
            //Id = deserialized.Data.
            ExternalId = Guid.Parse(s.ExternalId),
            Name = s.SchoolName,
            Phone = s.Phone,
            PostalCode = s.PostalCode,
            Street = s.Street,
            VisitingAddress = s.VisitingAddress,
            Locality = s.Locality,
            Email = s.Email,
            PrincipalName = s.Principal?.Fullname,
            PrincipalEmail = s.Principal?.Email,
            PrincipalPhone = s.Principal?.Tel

        };

        return schoolDetails;
    }

}
