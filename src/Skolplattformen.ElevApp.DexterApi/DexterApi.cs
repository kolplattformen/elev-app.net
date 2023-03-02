using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Skolplattformen.ElevApp.ApiInterface;
using Skolplattformen.ElevApp.ApiInterface.Models;
using Skolplattformen.ElevApp.DexterApi.Models.Internal;

namespace Skolplattformen.ElevApp.DexterApi
{
    public class DexterApi : IApi
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _email = string.Empty;
        private string _userId = string.Empty;
        private string _name = string.Empty;
        private readonly string _dexterLink = "https://kramfors.dexter-ist.com/Kramfors/services/resources/";
        private Guid _dummySchoolGuid = new Guid("00000000-0000-0000-0000-000000000001");
        private string _schoolName = String.Empty;

        private HttpClient _client;

        public ApiFeatures Features => ApiFeatures.Timetable | 
                                       ApiFeatures.SchoolDetails;

        public async Task LogInAsync(object loginCredentials)
        {
            if (loginCredentials is LoginCredentials lc)
            {
                _username = lc.Username;
                _password = lc.Password;
            }
            else { throw new Exception("Wrong login credentials"); }

            //var (username, password) = ((string, string))loginCredentials;
            //_username = username;
            //_password = password;

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.BaseAddress = new Uri(_dexterLink);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_username}:{_password}")));

            var rolesResponse = await GetAsync<GetRolesResponse>(Routes.GetRoles);
            var role = rolesResponse.RoleData.FirstOrDefault(x => x.RoleType == "STUDENT");
            if (role == null)
            {
                throw new Exception("No student role found");
            }
            _schoolName = role.UnitName!;
            _userId = role.Link.FirstOrDefault(x => x.Rel == "resources")?.Href?.Split('/').LastOrDefault() ?? string.Empty;

            var studentsResponse = await GetAsync<GetStudentsResponse>(Routes.GetStudents);
            var student = studentsResponse.Student.FirstOrDefault(x => x.UnitName == role.UnitName);
            if (student == null)
            {
                throw new Exception("No student found");
            }

            _name = student.FirstName! + " " + student.LastName!;

        }

        public async Task<ApiUser?> GetUserAsync()
        {
            return new ApiUser()
            {
                Name =_name,
                Id = Guid.NewGuid(),
                PrimarySchoolGuid = _dummySchoolGuid,
                PrimarySchoolName = _schoolName,
                Schools = new List<School>()
                {
                    new School()
                    {
                        Id = _dummySchoolGuid,
                        Name = _schoolName
                    }
                }
            };
        }

        public async Task<List<Teacher>> GetTeachersAsync()
        {
            return new List<Teacher>();
        }

        public async Task<SchoolDetails?> GetSchoolDetailsAsync(Guid schoolId)
        {
            return new SchoolDetails()
            {
                Id = schoolId,
                Name = _schoolName
            };
        }

        public async Task<List<TimeTableLesson>> GetTimetableAsync(int year, int week)
        {
            var startTime = ISOWeek.ToDateTime(year, week, DayOfWeek.Monday);
            var stopTime = ISOWeek.ToDateTime(year, week, DayOfWeek.Sunday);

            var GetScheduleResponse = await GetAsync<GetScheduleResponse>(Routes.GetSchedule(_userId, startTime, stopTime));

            var timeTableLessons = new List<TimeTableLesson>();

            foreach (var l in GetScheduleResponse.ScheduleEvent)
            {
                timeTableLessons.Add(
                    new TimeTableLesson
                    {
                        DayOfWeekNumber = (int)l.StartTime!.Value.DayOfWeek,
                        TimeStart = l.StartTime.Value.ToString("HH:mm"),
                        TimeEnd = l.EndTime!.Value.ToString("HH:mm"),
                        SubjectCode = l.Subject,
                        SubjectName = l.Subject,
                        TeacherCode = "",
                        TeacherName = "",
                        Location = l.RoomName
                    }
                );
            }

            return timeTableLessons;
        }

        public async Task<List<CalendarItem>> GetCalendarAsync(DateOnly date)
        {
            return new List<CalendarItem>();
        }

        public async Task<List<PlannedAbsenceItem>> GetPlannedAbsenceListAsync()
        {
            return new List<PlannedAbsenceItem>();
        }

        public void EnrichTimetableWithTeachers(List<TimeTableLesson> timetable, List<Teacher> teachers)
        {
            // N/A
        }

        public void EnrichTimetableWithCurriculum(List<TimeTableLesson> timetable)
        {
            // N/A
        }

        public void EnrichTeachersWithSubjects(List<Teacher> teachers, List<TimeTableLesson> timetable)
        {
            // N/A
        }

        public async Task<List<Meal>> GetMealsAsync(int year, int week)
        {
            return new List<Meal>();
        }

        public async Task<List<KalendariumItem>> GetKalendariumAsync()
        {
            return new List<KalendariumItem>();
        }

        public Task RefreshLoginAsync()
        {
            return Task.CompletedTask;
        }

        public ApiReadSuccessIndicator GetStatus(string part)
        {
            return ApiReadSuccessIndicator.Unknown;
        }

        public Dictionary<string, ApiReadSuccessIndicator> GetStatusAll()
        {
            return new Dictionary<string, ApiReadSuccessIndicator>();
        }
        
        private async Task<T> GetAsync<T>(string route)
        {
            var deserializeOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var response = await _client.GetAsync(route);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<T>(content, deserializeOptions);
            return result ?? default!;
        }
    }

    public class LoginCredentials
    {
   
        public string Username { get; set; }
        public string Password { get; set; }
    }
}