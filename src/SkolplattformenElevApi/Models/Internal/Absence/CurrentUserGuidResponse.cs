namespace SkolplattformenElevApi.Models.Internal.Absence;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
internal class CurrentUserGuidData
{
    public string CurrentUserGuid { get; set; }
}

internal class CurrentUserGuidResponse
{
    public object Error { get; set; }
    public CurrentUserGuidData Data { get; set; }
    public object Exception { get; set; }
    public List<object> Validation { get; set; }
    public DateTime SessionExpires { get; set; }
    public bool NeedSessionRefresh { get; set; }
}
