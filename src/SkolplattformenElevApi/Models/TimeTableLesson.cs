namespace SkolplattformenElevApi.Models;

public class TimeTableLesson
{
    public int DayOfWeekNumber { get; set; }
    public string TimeStart { get; set; }
    public string TimeEnd { get; set; }
    public string SubjectCode { get; set; }
    public string SubjectName { get; set; }
    public string TeacherCode { get; set; }
    public string? TeacherName { get; set; }
    public string Location { get; set; }
}