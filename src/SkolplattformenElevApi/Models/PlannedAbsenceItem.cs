namespace SkolplattformenElevApi.Models;

public class PlannedAbsenceItem
{
    public DateTime Created { get; set; }
    public string AbsenceId { get; set; }
    public string Comment { get; set; }
    public DateTime DateTimeFrom { get; set; }
    public DateTime DateTimeTo { get; set; }
    public bool IsFullDayAbsence { get; set; }
    public string ReasonDescription { get; set; }
    public string Reporter { get; set; }
}