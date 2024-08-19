using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Skolplattformen.ElevApp.ApiInterface;
using Skolplattformen.ElevApp.ApiInterface.Models;


namespace Skolplattformen.ElevApp.Data
{
    internal class CachedSchoolPlattformenElevApi: IApi
    {
        private readonly IApi _api;
        private readonly Dictionary<string, object> _cache = new Dictionary<string, object>();

        public ApiFeatures Features => _api.Features;

        public CachedSchoolPlattformenElevApi(IApi api)
        {
            _api = api;
        }

        
        public Task LogInAsync(object loginCredentials)
        {
            return _api.LogInAsync(loginCredentials);
        }

        public Task LogOutAsync()
        {
            return _api.LogOutAsync();
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

        public async Task RefreshLoginAsync()
        {
            await _api.RefreshLoginAsync();
            _cache.Clear();
        }

        public ApiReadSuccessIndicator GetStatus(string part)
        {
            return _api.GetStatus(part);
        }

        public Dictionary<string, ApiReadSuccessIndicator> GetStatusAll()
        {
            return _api.GetStatusAll();
        }
    }
}
