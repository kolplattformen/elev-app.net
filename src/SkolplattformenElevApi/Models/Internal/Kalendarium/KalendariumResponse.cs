using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SkolplattformenElevApi.Models.Internal.Kalendarium
{
    internal class KalendariumResponse
    {
        //[JsonPropertyName("odata.metadata")]
        //public string OdataMetadata { get; set; }

        [JsonPropertyName("odata.nextLink")]
        public string OdataNextLink { get; set; }

        [JsonPropertyName("value")]
        public List<KalendariumResponseItem> Value { get; set; }
    }

    internal class KalendariumResponseItem
    {
        //[JsonPropertyName("odata.type")]
        //public string OdataType { get; set; }

        //[JsonPropertyName("odata.id")]
        //public string OdataId { get; set; }

        //[JsonPropertyName("odata.etag")]
        //public string OdataEtag { get; set; }

        //[JsonPropertyName("odata.editLink")]
        //public string OdataEditLink { get; set; }

        //[JsonPropertyName("FileSystemObjectType")]
        //public int FileSystemObjectType { get; set; }

        //[JsonPropertyName("Id")]
        //public int Id { get; set; }

        //[JsonPropertyName("ServerRedirectedEmbedUri")]
        //public object ServerRedirectedEmbedUri { get; set; }

        //[JsonPropertyName("ServerRedirectedEmbedUrl")]
        //public string ServerRedirectedEmbedUrl { get; set; }

        //[JsonPropertyName("ContentTypeId")]
        //public string ContentTypeId { get; set; }

        [JsonPropertyName("Title")]
        public string Title { get; set; }

        //[JsonPropertyName("ComplianceAssetId")]
        //public object ComplianceAssetId { get; set; }

        [JsonPropertyName("Location")]
        public string Location { get; set; }

        [JsonPropertyName("Geolocation")]
        public object Geolocation { get; set; }

        [JsonPropertyName("EventDate")]
        public DateTime EventDate { get; set; }

        [JsonPropertyName("EndDate")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("fAllDayEvent")]
        public bool FAllDayEvent { get; set; }

        [JsonPropertyName("fRecurrence")]
        public bool FRecurrence { get; set; }

        //[JsonPropertyName("ParticipantsPickerId")]
        //public object ParticipantsPickerId { get; set; }

        //[JsonPropertyName("ParticipantsPickerStringId")]
        //public object ParticipantsPickerStringId { get; set; }

        [JsonPropertyName("Category")]
        public string Category { get; set; }

        [JsonPropertyName("FreeBusy")]
        public object FreeBusy { get; set; }

        [JsonPropertyName("Overbook")]
        public object Overbook { get; set; }

        //[JsonPropertyName("BannerUrl")]
        //public object BannerUrl { get; set; }

        //[JsonPropertyName("BannerImageUrl")]
        //public object BannerImageUrl { get; set; }

        //[JsonPropertyName("ID")]
        //public int ID { get; set; }

        [JsonPropertyName("Modified")]
        public DateTime Modified { get; set; }

        [JsonPropertyName("Created")]
        public DateTime Created { get; set; }

        //[JsonPropertyName("AuthorId")]
        //public int AuthorId { get; set; }

        //[JsonPropertyName("EditorId")]
        //public int EditorId { get; set; }

        //[JsonPropertyName("OData__UIVersionString")]
        //public string ODataUIVersionString { get; set; }

        //[JsonPropertyName("Attachments")]
        //public bool Attachments { get; set; }

        [JsonPropertyName("GUID")]
        public string GUID { get; set; }
    }
}
