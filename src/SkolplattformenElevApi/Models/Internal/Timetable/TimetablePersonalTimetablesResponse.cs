namespace SkolplattformenElevApi.Models.Internal.Timetable;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
internal class PersonalTimetableData
{
    public object Errors { get; set; }
    public GetPersonalTimetablesResponse GetPersonalTimetablesResponse { get; set; }
}

internal class GetPersonalTimetablesResponse
{
    public object TeacherTimetables { get; set; }
    public List<StudentTimetable> StudentTimetables { get; set; }
    public object ChildrenTimetables { get; set; }
}

internal class TimetablePersonalTimetablesResponse
{
    public object Error { get; set; }
    public PersonalTimetableData Data { get; set; }
    public object Exception { get; set; }
    public List<object> Validation { get; set; }
    public DateTime SessionExpires { get; set; }
    public bool NeedSessionRefresh { get; set; }
}

internal class StudentTimetable
{
    public string SchoolGuid { get; set; }
    public string UnitGuid { get; set; }
    public string SchoolID { get; set; }
    public string TimetableID { get; set; }
    public string PersonGuid { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

