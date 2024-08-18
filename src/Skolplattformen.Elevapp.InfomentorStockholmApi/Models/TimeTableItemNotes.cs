using System.Text.Json.Serialization;

namespace Skolplattformen.ElevApp.InfomentorStockholmApi.Models;

public class TimeTableItemNotes
{
    [JsonPropertyName("roomInfo")]
    public string RoomInfo { get; set; }

    [JsonPropertyName("timetableNotes")]
    public string TimetableNotes { get; set; }

    [JsonPropertyName("tutors")]
    public string Tutors { get; set; }
}