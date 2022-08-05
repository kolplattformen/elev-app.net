namespace SkolplattformenElevApi.Models.Internal.Absence;

internal class PlanedAbsenceData
{
    public List<PlannedAbsence>? PlannedAbsences { get; set; }
}

internal class PlannedAbsence
{
    public string? AbsenceId { get; set; }
    public int Week { get; set; }
    public DateTime DateFrom { get; set; }
    public object? DateTo { get; set; }
    public object? Reason { get; set; }
    public string? ReasonId { get; set; }
    public string? ReasonGuid { get; set; }
    public string? Comment { get; set; }
    public bool ShowComment { get; set; }
    public string? Reporter { get; set; }
    public bool FullDayAbsence { get; set; }
    public bool ReadOnly { get; set; }
    public string? AbsenceCreatorPersonGuid { get; set; }
    public DateTime AbsenceCreationTime { get; set; }
    public DateTime DateTimeFrom { get; set; }
    public DateTime DateTimeTo { get; set; }
    public bool IsActive { get; set; }
    public bool IsFullDayAbsence { get; set; }
    public bool HasAbsenceEndAcknowledge { get; set; }
    public bool HasAbsenceStartAcknowledge { get; set; }
    public bool NeedsAcknowledge { get; set; }
    public string? ReasonDescription { get; set; }
}

internal class PlannedAbsenceResponse
{
    public object? Error { get; set; }
    public PlanedAbsenceData? Data { get; set; }

}

