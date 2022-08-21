using System.Globalization;
using SkolplattformenElevApi.Models;
using SkolplattformenElevApi.Models.News;
using System.Reflection;
using System.Text.Json;

namespace SkolplattformenElevApi
{
    public class FakeApi: IApi
    {
        private FakeData.FakeData _fakeData;

        public FakeApi()
        {

            var assembly = Assembly.GetExecutingAssembly();

            var resourceName = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith("fakedata.json"));

            using (Stream stream = assembly.GetManifestResourceStream(resourceName)!)
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();

                _fakeData = JsonSerializer.Deserialize<FakeData.FakeData>(result)!;
            }

            AdjustCalenderDates();
            AdjustMealDates();
            AdjustPlannedAbsenceDates();

        }

        private void AdjustCalenderDates()
        {
            var today = DateTime.Today;
            var firstDayOdWeek = today.AddDays(-1 * (int)today.DayOfWeek);
            var daysSince = (today - new DateTime(2001, 01, 01)).TotalDays;
            _fakeData.CalendarItems[0].Start = firstDayOdWeek.AddDays(1).AddHours(14);
            _fakeData.CalendarItems[0].End = firstDayOdWeek.AddDays(1).AddHours(14).AddMinutes(30);

            _fakeData.CalendarItems[1].Start = firstDayOdWeek.AddDays(5).AddHours(10);
            _fakeData.CalendarItems[1].End = firstDayOdWeek.AddDays(5).AddHours(11).AddMinutes(30);
        }

        private void AdjustMealDates()
        {
            var today = DateTime.Today;
            var firstDayOdWeek = today.AddDays(-1 * (int)today.DayOfWeek);
            var isoWeek = ISOWeek.GetWeekOfYear(today);
            var startDate = firstDayOdWeek.AddDays(+1).AddHours(12);
            foreach (var meal in _fakeData.Meals)
            {
                var parts = meal.Title.Split(" ");
                parts[^1] = isoWeek.ToString();

                meal.Title = string.Join(" ", parts);
                meal.Date = startDate;
                startDate = startDate.AddDays(1);
                
            }
        }

        private void AdjustPlannedAbsenceDates()
        {
            var today = DateTime.Today;
            var firstDayOdWeek = today.AddDays(-1 * (int)today.DayOfWeek);
            _fakeData.PlannedAbsenceItems[0].Created = firstDayOdWeek.AddDays(2).AddHours(8);
            _fakeData.PlannedAbsenceItems[0].DateTimeFrom = firstDayOdWeek.AddDays(2).AddHours(9);
            _fakeData.PlannedAbsenceItems[0].DateTimeTo = firstDayOdWeek.AddDays(2).AddHours(19);
            _fakeData.PlannedAbsenceItems[0].IsFullDayAbsence = true;
        }

        public async Task LogInAsync(string email, string username, string password)
        {
            await Task.Delay(1000);
        }

        public Task<List<NewsListItem>> GetNewsItemListAsync(int itemsToGet = 5)
        {
            return Task.FromResult(new List<NewsListItem>());
        }

        public Task<NewsItem> GetNewsItemAsync(string path)
        {
            return Task.FromResult(new NewsItem());
        }

        public Task<ApiUser?> GetUserAsync()
        {
            return Task.FromResult(_fakeData.ApiUser);
        }

        public Task<List<Teacher>> GetTeachersAsync()
        {
            return Task.FromResult(_fakeData.Teachers);
        }

        public Task<SchoolDetails?> GetSchoolDetailsAsync(Guid schoolId)
        {
            return Task.FromResult(_fakeData.SchoolDetails.FirstOrDefault());
        }

        public Task<List<TimeTableLesson>> GetTimetableAsync(int year, int week)
        {
            return Task.FromResult(_fakeData.TimeTable);
        }

        public Task<List<CalendarItem>> GetCalendarAsync(DateOnly date)
        {
            var dateTime = date.ToDateTime(TimeOnly.Parse("00:00"));
            return Task.FromResult(_fakeData.CalendarItems.Where(ci => ci.Start.Date <= dateTime.Date && ci.End.Date >= dateTime.Date).ToList());
        }

        public Task<List<PlannedAbsenceItem>> GetPlannedAbsenceListAsync()
        {
            return Task.FromResult(_fakeData.PlannedAbsenceItems);
        }

        public Task<List<Meal>> GetMealsAsync(int year, int week)
        {
            return Task.FromResult(_fakeData.Meals);
        }

        public Task<List<KalendariumItem>> GetKalendariumAsync()
        {
            throw new NotImplementedException();
        }

        public void EnrichTimetableWithTeachers(List<TimeTableLesson> timetable, List<Teacher> teachers)
        {
            Utils.Enrichers.EnrichTimetableWithTeachers(timetable, teachers);
        }

        public void EnrichTimetableWithCurriculum(List<TimeTableLesson> timetable)
        {
           Utils.Enrichers.EnrichTimetableWithCurriculum(timetable);
        }

        public void EnrichTeachersWithSubjects(List<Teacher> teachers, List<TimeTableLesson> timetable)
        {
            Utils.Enrichers.EnrichTeachersWithSubjects(teachers, timetable);
        }
    }
}
