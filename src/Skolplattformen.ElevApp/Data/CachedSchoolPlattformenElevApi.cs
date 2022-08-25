using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SkolplattformenElevApi;
using SkolplattformenElevApi.Models;
using SkolplattformenElevApi.Models.News;

namespace Skolplattformen.ElevApp.Data
{
    internal class CachedSchoolPlattformenElevApi: IApi
    {
        private readonly IApi _api;
        private readonly Dictionary<string, object> _cache = new Dictionary<string, object>();

        public CachedSchoolPlattformenElevApi(IApi api)
        {
            _api = api;
        }

        public Task LogInAsync(string email, string username, string password)
        {
            return _api.LogInAsync(email, username, password);
        }

        public async Task<List<NewsListItem>> GetNewsItemListAsync(int numItems = 5)
        {
            var key = $"newsList_{numItems}";
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = await _api.GetNewsItemListAsync(numItems);
            }
            return _cache[key] as List<NewsListItem>;
        }

        public async Task<NewsItem> GetNewsItemAsync(string path)
        {
            var key = $"newsItem_{path}";
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = await _api.GetNewsItemAsync(path);
            }
            return _cache[key] as NewsItem;
        }

        public async Task<ApiUser> GetUserAsync()
        {
            var key = "user";
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = await _api.GetUserAsync();
            }
            return _cache[key] as ApiUser;
        }
        
       

        public async Task<List<Teacher>> GetTeachersAsync()
        {
            var key = "teachers";
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = await _api.GetTeachersAsync();
            }
            return _cache[key] as List<Teacher>;
        }

        public async Task<SchoolDetails> GetSchoolDetailsAsync(Guid schoolId)
        {
            var key = $"schoolDetails_{schoolId}";
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = await _api.GetSchoolDetailsAsync(schoolId);
            }
            return _cache[key] as SchoolDetails;
        }

        public async Task<List<TimeTableLesson>> GetTimetableAsync(int year, int week)
        {
            var key = $"timetable_{year}_{week}";
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = await _api.GetTimetableAsync(year, week);
            }
            return _cache[key] as List<TimeTableLesson>;
        }

        public async Task<List<CalendarItem>> GetCalendarAsync(DateOnly date)
        {
            var key = $"calendar_{date}";
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = await _api.GetCalendarAsync(date);
            }
            return _cache[key] as List<CalendarItem>;
        }

        public async Task<List<PlannedAbsenceItem>> GetPlannedAbsenceListAsync()
        {
            var key = "plannedAbsenceList";
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = await _api.GetPlannedAbsenceListAsync();
            }
            return _cache[key] as List<PlannedAbsenceItem>;
        }

        public void EnrichTimetableWithTeachers(List<TimeTableLesson> timetable, List<Teacher> teachers)
        {
            _api.EnrichTimetableWithTeachers(timetable, teachers);
        }

        public void EnrichTimetableWithCurriculum(List<TimeTableLesson> timetable)
        {
            _api.EnrichTimetableWithCurriculum(timetable);
        }

        public void EnrichTeachersWithSubjects(List<Teacher> teachers, List<TimeTableLesson> timetable)
        {
            _api.EnrichTeachersWithSubjects(teachers, timetable);
        }

        public async Task<List<Meal>> GetMealsAsync(int year, int week)
        {
            var key = $"meals_{year}_{week}";
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = await _api.GetMealsAsync(year, week);
            }
            return _cache[key] as List<Meal>;
        }

        public async Task<List<KalendariumItem>> GetKalendariumAsync()
        {
            var key = $"kalendarium";
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = await _api.GetKalendariumAsync();
            }
            return _cache[key] as List<KalendariumItem>;
        }
    }
}
