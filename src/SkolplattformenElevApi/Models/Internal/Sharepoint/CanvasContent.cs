namespace SkolplattformenElevApi.Models.Internal.Sharepoint
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    internal class CustomMetadata
    {
        public ImageSource imageSource { get; set; }
    }

    internal class HtmlStrings
    {
    }

    internal class ImageSource
    {
        public string siteid { get; set; }
        public string webid { get; set; }
        public string listid { get; set; }
        public string uniqueid { get; set; }
        public string width { get; set; }
        public string height { get; set; }
    }

    internal class ImageSources
    {
        public string imageSource { get; set; }
    }

    internal class Links
    {
    }

    internal class PageSettingsSlice
    {
        public bool isDefaultDescription { get; set; }
        public bool isDefaultThumbnail { get; set; }
        public bool isSpellCheckEnabled { get; set; }
    }

    internal class Position
    {
        public int controlIndex { get; set; }
        public int sectionIndex { get; set; }
        public int sectionFactor { get; set; }
        public int zoneIndex { get; set; }
        public int layoutIndex { get; set; }
    }

    internal class Properties
    {
        public int imageSourceType { get; set; }
        public string altText { get; set; }
        public string linkUrl { get; set; }
        public string overlayText { get; set; }
        public string fileName { get; set; }
        public string siteId { get; set; }
        public string webId { get; set; }
        public string listId { get; set; }
        public string uniqueId { get; set; }
        public int imgWidth { get; set; }
        public int imgHeight { get; set; }
        public string alignment { get; set; }
        public bool fixAspectRatio { get; set; }
    }

    internal class CanvasContent
    {
        public int controlType { get; set; }
        public string id { get; set; }
        public Position position { get; set; }
        public bool addedFromPersistedData { get; set; }
        public string innerHTML { get; set; }
        public string webPartId { get; set; }
        public int? reservedHeight { get; set; }
        public int? reservedWidth { get; set; }
        public WebPartData webPartData { get; set; }
        public PageSettingsSlice pageSettingsSlice { get; set; }
    }

    internal class SearchablePlainTexts
    {
        public string captionText { get; set; }
    }

    internal class ServerProcessedContent
    {
        public HtmlStrings htmlStrings { get; set; }
        public SearchablePlainTexts searchablePlainTexts { get; set; }
        public ImageSources imageSources { get; set; }
        public Links links { get; set; }
        public CustomMetadata customMetadata { get; set; }
    }

    internal class WebPartData
    {
        public string id { get; set; }
        public string instanceId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public List<object> audiences { get; set; }
        public ServerProcessedContent serverProcessedContent { get; set; }
        public string dataVersion { get; set; }
        public Properties properties { get; set; }
    }


}
