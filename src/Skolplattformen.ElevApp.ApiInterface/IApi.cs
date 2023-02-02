using Skolplattformen.ElevApp.ApiInterface.Models;

namespace Skolplattformen.ElevApp.ApiInterface
{
    public interface IApi
    {
        Task LogInAsync(string email, string username, string password);
        Task<ApiUser?> GetUserAsync();
        Task<List<Teacher>> GetTeachersAsync();
        Task<SchoolDetails?> GetSchoolDetailsAsync(Guid schoolId);
        Task<List<TimeTableLesson>> GetTimetableAsync(int year, int week);
        Task<List<CalendarItem>> GetCalendarAsync(DateOnly date);
        Task<List<PlannedAbsenceItem>> GetPlannedAbsenceListAsync();
        void EnrichTimetableWithTeachers(List<TimeTableLesson> timetable, List<Teacher> teachers);
        void EnrichTimetableWithCurriculum(List<TimeTableLesson> timetable);
        void EnrichTeachersWithSubjects(List<Teacher> teachers, List<TimeTableLesson> timetable);
        Task<List<Meal>> GetMealsAsync(int year, int week);
        Task<List<KalendariumItem>> GetKalendariumAsync();
        Task RefreshLoginAsync();
        ApiReadSuccessIndicator GetStatus(string part);
        Dictionary<string, ApiReadSuccessIndicator> GetStatusAll();
        ApiFeatures Features { get; }
    }

    public enum ApiReadSuccessIndicator
    {
        Unknown = 0,
        Success = 1,
        NoData = 2,
        Error = 3
    }

    [Flags]
    public enum ApiFeatures
    {
        None = 0,
        Timetable = 1,
        Calendar = 2,
        PlannedAbsence = 4,
        Meals = 8,
        Kalendarium = 16,
        SchoolDetails = 32,
        Teachers = 64,
    }

}