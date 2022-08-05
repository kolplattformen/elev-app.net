namespace SkolplattformenElevApi.Models.Internal.Timetable;
internal class TimetableRenderKeyData
{
    public string Key { get; set; }
}

internal class TimetableRenderKeyResponse
{
    public object Error { get; set; }
    public TimetableRenderKeyData Data { get; set; }
    public object Exception { get; set; }
    public List<object> Validation { get; set; }
    public DateTime SessionExpires { get; set; }
    public bool NeedSessionRefresh { get; set; }
}