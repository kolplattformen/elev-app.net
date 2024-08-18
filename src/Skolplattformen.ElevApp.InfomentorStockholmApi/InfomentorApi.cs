using Skolplattformen.ElevApp.ApiInterface;
using Skolplattformen.ElevApp.ApiInterface.Models;
using System.Globalization;
using System.Text.Json;
using Skolplattformen.ElevApp.InfomentorStockholmApi.Models;

namespace Skolplattformen.ElevApp.InfomentorStockholmApi
{
    public partial class InfomentorApi : IApi
    {

        public ApiFeatures Features => ApiFeatures.Timetable;
        //| ApiFeatures.Calendar
        //| ApiFeatures.PlannedAbsence
        //| ApiFeatures.Meals
        //| ApiFeatures.Kalendarium
        //| ApiFeatures.SchoolDetails
        //| ApiFeatures.Teachers;

        public async Task<ApiUser?> GetUserAsync()
        {
            var response = await _httpClient.PostAsync("https://hub.infomentor.se/NotificationApp/NotificationApp/appData", null) ;
            var content = await response.Content.ReadAsStringAsync();

            var name = string.Empty;

            if (content.Contains("pupilName"))
            {
                try
                {
                    name = RegExp("\"pupilName\":\"([^\"]*)\"", content);
                }
                catch
                {
                    // Do nothing
                }
                
            }

            return new ApiUser{Name = name};
            
        }

        public async Task<List<Teacher>> GetTeachersAsync()
        {
            return new List<Teacher>();
            throw new NotImplementedException();
        }

        public async Task<SchoolDetails?> GetSchoolDetailsAsync(Guid schoolId)
        {
            return new SchoolDetails();
            throw new NotImplementedException();
        }

        public async Task<List<TimeTableLesson>> GetTimetableAsync(int year, int week)
        {

            var startTime = ISOWeek.ToDateTime(year, week, DayOfWeek.Monday);
            var stopTime = ISOWeek.ToDateTime(year, week, DayOfWeek.Sunday);

            var response = await _httpClient.PostAsync(Routes.GetTimeTable, new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "UTCOffset", Math.Ceiling((DateTime.UtcNow - DateTime.Now).TotalMinutes).ToString(CultureInfo.InvariantCulture)}, // doesn't seem to do anything
                { "start", startTime.ToString("yyyy-MM-dd") },
                { "end", stopTime.ToString("yyyy-MM-dd") }
            }));
            var timetableJsonString = await response.Content.ReadAsStringAsync();
            var timetableJson = JsonSerializer.Deserialize<List<TimeTableItem>>(timetableJsonString)
                ?? new List<TimeTableItem>();

            var timetable = new List<TimeTableLesson>();

            foreach (var item in timetableJson)
            {
                var lesson = new TimeTableLesson
                {
                    DayOfWeekNumber = (int)item.Start.DayOfWeek,
                    TimeStart = item.StartTime,
                    TimeEnd = item.EndTime,
                    SubjectCode = item.Title,
                    SubjectName = item.Title,
                    TeacherCode = item.Notes.Tutors,
                    TeacherName = item.Notes.Tutors,
                    Location = item.Notes.RoomInfo

                };
                timetable.Add(lesson);
            }

            return timetable;
        }

        public async Task<List<CalendarItem>> GetCalendarAsync(DateOnly date)
        {
            return new List<CalendarItem>();
            throw new NotImplementedException();
        }

        public async Task<List<PlannedAbsenceItem>> GetPlannedAbsenceListAsync()
        {
            return new List<PlannedAbsenceItem>();
            throw new NotImplementedException();
        }

        public void EnrichTimetableWithTeachers(List<TimeTableLesson> timetable, List<Teacher> teachers)
        {
            // do nothing
        }

        public void EnrichTimetableWithCurriculum(List<TimeTableLesson> timetable)
        {
            // do nothing
        }

        public void EnrichTeachersWithSubjects(List<Teacher> teachers, List<TimeTableLesson> timetable)
        {
            // do nothing
        }

        public async Task<List<Meal>> GetMealsAsync(int year, int week)
        {
            return new List<Meal>();
            throw new NotImplementedException();
        }

        public async Task<List<KalendariumItem>> GetKalendariumAsync()
        {
            return new List<KalendariumItem>();
            throw new NotImplementedException();
        }

        public async Task RefreshLoginAsync()
        {
            return;
            throw new NotImplementedException();
        }

        public ApiReadSuccessIndicator GetStatus(string part)
        {
            return ApiReadSuccessIndicator.Unknown;
            throw new NotImplementedException();
        }

        public Dictionary<string, ApiReadSuccessIndicator> GetStatusAll()
        {
            return new Dictionary<string, ApiReadSuccessIndicator>();
            throw new NotImplementedException();
        }

    
      
    }
}
