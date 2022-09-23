namespace Skolplattformen.ElevApp.Models
{
    public enum TodayItemMark
    {
        Standard = 0,
        Lesson = 1,
        Absence = 2,
        Kalendarium = 3,
        Calendar = 4
    }
    public class TodayItem
    {
        public string StartTime { get; set; } = "00:00";
        public string EndTime { get; set; } = "00:00";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public TodayItemMark Mark { get; set; } = TodayItemMark.Standard;

        public bool IsAllDay => StartTime == "00:00" && EndTime == "00:00";

    }
}
