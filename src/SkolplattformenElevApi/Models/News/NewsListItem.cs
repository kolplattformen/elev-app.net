namespace SkolplattformenElevApi.Models.News;

public class NewsListItem
{
    public string? ContentType { get; set; }
    public string? ContentTypeId { get; set; }
    public string? Title { get; set; }
    public string? EditorOwsUser { get; set; }
    public string? ModifiedBy { get; set; }
    public string? LastModifiedBy { get; set; }
    public string? Path { get; set; }
    public string? SiteName { get; set; }
    public string? SiteTitle { get; set; }
    public string? PictureThumbnailUrl { get; set; }
    public DateTime LastModifiedTime { get; set; }
    public string? ListID { get; set; } // Guid
    public string? ListItemId { get; set; }
    public string? SiteID { get; set; }
    public string? WebId { get; set; } // Guid
    public string? UniqueID { get; set; } // Guid
    public string? SitePath { get; set; }
    public string? UserName { get; set; }
    public string? SPWebUrl { get; set; }
    public long DocId { get; set; }
}