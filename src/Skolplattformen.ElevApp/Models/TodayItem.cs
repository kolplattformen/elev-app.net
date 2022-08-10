namespace Skolplattformen.ElevApp.Models
{
    public enum TodayItemMark
    {
        Standard = 0,
        Warning = 1,
        AllDay = 2
    }
    public class TodayItem
    {
        public string StartTime { get; set; } = "00:00";
        public string EndTime { get; set; } = "00:00";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public TodayItemMark Mark { get; set; } = TodayItemMark.Standard;

        public Color Color => Mark switch
        {
            TodayItemMark.AllDay => Microsoft.Maui.Graphics.Color.FromRgb(255, 0, 0),
            TodayItemMark.Warning => Microsoft.Maui.Graphics.Color.FromRgb(255, 255, 0),
            _ => Microsoft.Maui.Graphics.Color.FromRgba(255, 255, 255, 0)
        };
    }
}
