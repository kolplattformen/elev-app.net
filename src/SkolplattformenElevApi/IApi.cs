using SkolplattformenElevApi.Models;
using SkolplattformenElevApi.Models.News;

namespace SkolplattformenElevApi
{
    public interface IApi
    {
        Task LogInAsync(string email, string username, string password);
        Task<List<NewsListItem>> GetNewsItemListAsync(int itemsToGet = 5);
        Task<NewsItem> GetNewsItemAsync(string path);
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
    }
}