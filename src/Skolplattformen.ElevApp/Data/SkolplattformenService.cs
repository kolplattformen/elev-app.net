using System.Globalization;
using SkolplattformenElevApi;
using SkolplattformenElevApi.Models;

namespace Skolplattformen.ElevApp.Data;

public enum ApiKind
{
    Skolplattformen = 1,
    FakeData = 2
}


public class SkolplattformenService
{
    private IApi _api;
    private DateTime _loggedInTime = DateTime.MinValue;
    public string LoggedInName { get; set; } = string.Empty;
    public bool IsLoggedIn => _loggedInTime > DateTime.UtcNow.AddMinutes(-29);
    private ApiKind _apiKind = ApiKind.Skolplattformen;


    public SkolplattformenService()
    {
        _api = new CachedSchoolPlattformenElevApi(new SkolplattformenElevApi.Api());

    }

    public void SelectApi(ApiKind apiKind)
    {
        _apiKind = apiKind;

        LogOut();
    }

    public void LogOut()
    {
        _loggedInTime = DateTime.MinValue;
        if (_apiKind == ApiKind.Skolplattformen)
        {
            _api = new CachedSchoolPlattformenElevApi(new SkolplattformenElevApi.Api());
        }
        else
        {
            _api = new FakeApi();
        }
    }

    public async Task LogInAsync(string email, string username, string password)
    {
        await _api.LogInAsync(email, username, password);
        _loggedInTime = DateTime.UtcNow;
        var user = await _api.GetUserAsync();
        LoggedInName = user?.Name ?? email;
    }

    //public Task<List<NewsListItem>> GetNewsItemList()
    //{
    //    return _api.GetNewsItemListAsync();
    //}

    //public Task<NewsItem> GetNewsItem(string path)
    //{
    //    return _api.GetNewsItemAsync(path);
    //}

    public async Task<List<Teacher>> GetTeachersAsync()
    {
        // Get timetable from two weeks, so we don't miss subjects while holidays etc.
        var today = DateTime.Now;
        var week = ISOWeek.GetWeekOfYear(today);
        var year = ISOWeek.GetYear(today);
        var timetable = await _api.GetTimetableAsync(year, week);
        var nextWeek = DateTime.Now.AddDays(7);
        var nextWeekWeek = ISOWeek.GetWeekOfYear(nextWeek);
        var nextWeekYear = ISOWeek.GetYear(nextWeek);
        var nextWeekTimetable = await _api.GetTimetableAsync(nextWeekYear, nextWeekWeek);
        timetable.AddRange(nextWeekTimetable);

        var teachers = await _api.GetTeachersAsync();
        _api.EnrichTimetableWithCurriculum(timetable);
        _api.EnrichTeachersWithSubjects(teachers, timetable);
        return teachers;
    }

    public async Task<List<TimeTableLesson>> GetTimetableAsync(int year, int week)
    {

        var teachers = await _api.GetTeachersAsync();
        var lessons = await _api.GetTimetableAsync(year, week);
        _api.EnrichTimetableWithCurriculum(lessons);
        _api.EnrichTimetableWithTeachers(lessons, teachers);

        return lessons;
    }

    public async Task<List<TimeTableLesson>> GetTimetableAsync(DateTime day)
    {
        var week = ISOWeek.GetWeekOfYear(day);
        var year = ISOWeek.GetYear(day);
        var dayOdWeek = (int)day.DayOfWeek;
        var teachers = await _api.GetTeachersAsync();
        var lessons = await _api.GetTimetableAsync(year, week);
        var todaysLessons = lessons.Where(l => l.DayOfWeekNumber == dayOdWeek).ToList();
        _api.EnrichTimetableWithCurriculum(todaysLessons);
        _api.EnrichTimetableWithTeachers(todaysLessons, teachers);
        
        return todaysLessons;
    }

    public async Task<List<Meal>> GetMealsAsync(int year, int week)
    {
        if (Settings.UseSkolmatenSe)
        {
            return await SkolmatenSeService.GetWeekAsync(Settings.SkolmatenSeSchoolName, year, week);
        }

        return await _api.GetMealsAsync(year, week);
    }

    public async Task<Meal?> GetMealAsync(DateTime day)
    {
        var week = ISOWeek.GetWeekOfYear(day);
        var year = ISOWeek.GetYear(day);
        List<Meal> meals;
        if (Settings.UseSkolmatenSe)
        {
            meals = await SkolmatenSeService.GetWeekAsync(Settings.SkolmatenSeSchoolName, year, week);
        }
        else
        {
            meals = await _api.GetMealsAsync(year, week);
        }
        return meals.FirstOrDefault(m => m.Date.Date == day.Date);
    }

    public Task<ApiUser?> GetUserAsync()
    {
        return _api.GetUserAsync();
    }

    public Task<SchoolDetails> GetSchoolDetailsAsync(Guid schoolId)
    {
        return _api.GetSchoolDetailsAsync(schoolId);
    }

    public Task<List<CalendarItem>> GetCalendarAsync(DateTime date)
    {
        return _api.GetCalendarAsync(new DateOnly(date.Year,date.Month,date.Day));
    }

    public async Task<List<PlannedAbsenceItem>> GetPlannedAbsenceAsync(DateTime date)
    { 
        var all = await  _api.GetPlannedAbsenceListAsync();
        var today = all.Where(x => x.DateTimeFrom.Date == date.Date || x.DateTimeTo.Date == date.Date);
        return today.ToList();
    }
}
