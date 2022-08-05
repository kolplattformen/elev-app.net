namespace SkolplattformenElevApi.Models;

public class CalendarItem
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Location { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public bool IsAllDay { get; set; } // start.minutes.utc == 00 and end-start == 24 hours ?
    public string WebLink { get; set; }
}