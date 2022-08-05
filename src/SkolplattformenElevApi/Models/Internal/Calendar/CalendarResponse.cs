using System.Text.Json.Serialization;

namespace SkolplattformenElevApi.Models.Internal.Calendar;

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
internal class CalendarItemData
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("Title")]
    public string Title { get; set; }

    [JsonPropertyName("Location")]
    public string Location { get; set; }

    [JsonPropertyName("Start")]
    public DateTime Start { get; set; }

    [JsonPropertyName("End")]
    public DateTime End { get; set; }

    [JsonPropertyName("WebLink")]
    public string WebLink { get; set; }

    [JsonPropertyName("extensions")]
    public object Extensions { get; set; }
}

internal class CalendarResponse
{
    [JsonPropertyName("Success")]
    public bool Success { get; set; }

    [JsonPropertyName("Error")]
    public object Error { get; set; }

    [JsonPropertyName("Data")]
    public List<CalendarItemData>? Data { get; set; }
}


