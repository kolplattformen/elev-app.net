using Skolplattformen.ElevApp.ApiInterface.Models;

namespace Skolplattformen.ElevApp.FakeApi.FakeData
{

    internal class FakeData
    {

        public  ApiUser ApiUser { get; set; }
        public List<CalendarItem> CalendarItems { get; set; }
        public List<PlannedAbsenceItem> PlannedAbsenceItems { get; set; }
        public List<SchoolDetails> SchoolDetails { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<TimeTableLesson> TimeTable { get; set; }
        public List<Meal> Meals { get; set; }
        public List<KalendariumItem> Kalendarium { get; set; }
    }

}
