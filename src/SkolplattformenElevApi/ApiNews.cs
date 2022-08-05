using System.Net.Http.Headers;
using System.Text.Json;
using SkolplattformenElevApi.Models.Internal.Sharepoint;
using SkolplattformenElevApi.Models.News;

namespace SkolplattformenElevApi
{
    public partial class Api
    {
        public async Task<List<NewsListItem>> GetNewsItemListAsync(int itemsToGet = 5)
        {
            var query = "{ \"request\": {\"Querytext\":\"\",\"QueryTemplate\":\"{searchterms} -SiteTitle:\\\"Användarstöd\\\" AND (LastModifiedTime=\\\"this year\\\" OR LastModifiedTime=\\\"last year\\\") AND  ((ContentTypeId:0x0101009D1CB255DA76424F860D91F20E6C4118* AND PromotedState=2 AND NOT ContentTypeId:0x0101009D1CB255DA76424F860D91F20E6C4118002A50BFCFB7614729B56886FADA02339B00873E381CC9DD4F2E808A377A72C311BB*))\",\"ClientType\":\"HighlightedContentWebPart\",\"RowLimit\":" + itemsToGet + ",\"RowsPerPage\":" + itemsToGet + ",\"TimeZoneId\":4,\"SelectProperties\":[\"ContentType\",\"ContentTypeId\",\"Title\",\"EditorOwsUser\",\"ModifiedBy\",\"LastModifiedBy\",\"FileExtension\",\"FileType\",\"Path\",\"SiteName\",\"SiteTitle\",\"PictureThumbnailURL\",\"DefaultEncodingURL\",\"LastModifiedTime\",\"ListID\",\"ListItemID\",\"SiteID\",\"WebId\",\"UniqueID\",\"LastModifiedTime\",\"SitePath\",\"UserName\",\"ProfileImageSrc\",\"Name\",\"Initials\",\"WebPath\",\"PreviewUrl\",\"IconUrl\",\"AccentColor\",\"CardType\",\"TipActionLabel\",\"TipActionButtonIcon\",\"ClassName\",\"TelemetryProperties\",\"ImageOverlapText\",\"ImageOverlapTextAriaLabel\",\"SPWebUrl\",\"IsExternalContent\",\"MediaServiceMetadata\",\"LastModifiedTimeForRetention\"],\"Properties\":[{\"Name\":\"TrimSelectProperties\",\"Value\":{\"StrVal\":\"1\",\"QueryPropertyValueTypeIndex\":1}},{\"Name\":\"EnableDynamicGroups\",\"Value\":{\"BoolVal\":\"True\",\"QueryPropertyValueTypeIndex\":3}},{\"Name\":\"EnableMultiGeoSearch\",\"Value\":{\"BoolVal\":\"False\",\"QueryPropertyValueTypeIndex\":3}}],\"SortList\":[{\"Property\":\"LastModifiedTime\",\"Direction\":1}],\"SourceId\":\"8413CD39-2156-4E00-B54D-11EFD9ABDB89\",\"TrimDuplicates\":false} }";
            var temp_url = "https://elevstockholm.sharepoint.com/sites/skolplattformen/_api/search/postquery";
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(temp_url),
                Method = HttpMethod.Post,
                Headers =
                {
                    { "odata-version", "3.0" },
                    { "originalcorrelationid", _sharePointRequestGuid },
                    { "Referer", "https://elevstockholm.sharepoint.com/sites/skolplattformen/" },
                    { "SdkVersion", "SPFx/ContentRollupWebPart/daf0b71c-6de8-4ef7-b511-faae7c388708" },
                    { "x-requestdigest", _formDigestValue },
                    { "Origin", "https://elevstockholm.sharepoint.com" },
                },
                Content = new StringContent(query)
            };
            request.Headers.TryAddWithoutValidation("accept", new[] { "application/json;odata=nometadata" });
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;charset=utf-8");

            var temp_res = await _httpClient.SendAsync(request);
            var temp_content = await temp_res.Content.ReadAsStringAsync();

            var deserialized = JsonSerializer.Deserialize<PostQueryResponse>(temp_content);

            var list = new List<NewsListItem>();

            foreach (var item in deserialized.PrimaryQueryResult.RelevantResults.Table.Rows)
            {
                list.Add(RowToNewsListItem(item));
            }

            return list;
        }

        public async Task<NewsItem> GetNewsItemAsync(string path)
        {
            var temp_url = path + "?as=json";
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(temp_url),
                Method = HttpMethod.Get,
                Headers =
                {
                    { "odata-version", "4.0" },
                    { "originalcorrelationid", _sharePointRequestGuid },
                    { "Referer", path },
                    { "SdkVersion", "SPFx/ContentRollupWebPart/daf0b71c-6de8-4ef7-b511-faae7c388708" },
                  //  { "x-requestdigest", _formDigestValue },
                    { "Origin", "https://elevstockholm.sharepoint.com" },
                },
            };
            request.Headers.TryAddWithoutValidation("accept", new[] { "application/json;odata=nometadata" });
      //      request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;charset=utf-8");

            var temp_res = await _httpClient.SendAsync(request);
            var temp_content = await temp_res.Content.ReadAsStringAsync();

            var deserialized = JsonSerializer.Deserialize<NewsItemResponse>(temp_content);
            var deserializedCanvasContent = JsonSerializer.Deserialize<List<CanvasContent>>(deserialized.page.Content.CanvasContent1);


            var newsItem = new NewsItem();
            newsItem.InnerHtml = CanvasContentToHtml(deserializedCanvasContent);
            newsItem.Title = deserialized.item.Title;
            newsItem.Description = deserialized.item.Description;
            newsItem.Published = deserialized.item.FirstPublishedDate;
            newsItem.Modified = deserialized.item.Modified;
            newsItem.BannerImage = deserialized.item.BannerImageUrl.Url;
            newsItem.AuthorName = string.Empty; //TODO where is it?
            

            return newsItem;
        }

        private string CanvasContentToHtml(List<CanvasContent> ccontent)
        {
            var html = string.Empty;

            foreach (var cc in ccontent)
            {
                switch (cc.controlType)
                {
                    case 1: break; // TODO: find out what what it is
                    case 2: break;
                    case 3:
                        html += "<img src=\"" + cc.webPartData.serverProcessedContent.imageSources.imageSource + "\">"; 
                        break;
                    case 4: html += cc.innerHTML;
                        break;
                    default: break;

                }
            }
            
            return html;
        }

        private NewsListItem RowToNewsListItem(Row row)
        {
            return new NewsListItem
            {
                ContentType = row.Cells.FirstOrDefault(c => c.Key == "ContentType")?.Value?.ToString(),
                ContentTypeId = row.Cells.FirstOrDefault(c => c.Key == "ContentTypeId")?.Value?.ToString(),
                Title = row.Cells.FirstOrDefault(c => c.Key == "Title")?.Value?.ToString(),
                EditorOwsUser = row.Cells.FirstOrDefault(c => c.Key == "EditorOwsUser")?.Value?.ToString(),
                ModifiedBy = row.Cells.FirstOrDefault(c => c.Key == "ModifiedBy")?.Value?.ToString(),
                LastModifiedBy = row.Cells.FirstOrDefault(c => c.Key == "LastModifiedBy")?.Value?.ToString(),
                Path = row.Cells.FirstOrDefault(c => c.Key == "Path")?.Value?.ToString(),
                SiteName = row.Cells.FirstOrDefault(c => c.Key == "SiteName")?.Value?.ToString(),
                SiteTitle = row.Cells.FirstOrDefault(c => c.Key == "SiteTitle")?.Value?.ToString(),
                PictureThumbnailUrl = row.Cells.FirstOrDefault(c => c.Key == "PictureThumbnailUrl")?.Value?.ToString(),
                LastModifiedTime =
                    DateTime.Parse(row.Cells.FirstOrDefault(c => c.Key == "LastModifiedTime").Value.ToString()),
                ListID = row.Cells.FirstOrDefault(c => c.Key == "ListId")?.Value?.ToString(),
                ListItemId = row.Cells.FirstOrDefault(c => c.Key == "ListItemId")?.Value?.ToString(),
                SiteID = row.Cells.FirstOrDefault(c => c.Key == "SiteId")?.Value?.ToString(),
                WebId = row.Cells.FirstOrDefault(c => c.Key == "WebId")?.Value?.ToString(),
                UniqueID = row.Cells.FirstOrDefault(c => c.Key == "UniqueID")?.Value?.ToString(),
                SitePath = row.Cells.FirstOrDefault(c => c.Key == "SitePath")?.Value?.ToString(),
                UserName = row.Cells.FirstOrDefault(c => c.Key == "UserName")?.Value?.ToString(),
                SPWebUrl = row.Cells.FirstOrDefault(c => c.Key == "SPWebUrl")?.Value?.ToString(),
     //           DocId = Int64.Parse((row.Cells.FirstOrDefault(c => c.Key == "SiteId")?.Value).ToString()),

            };
        }
    }
}
