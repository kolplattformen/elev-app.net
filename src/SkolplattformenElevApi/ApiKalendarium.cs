using System.Net.Http.Headers;
using System.Text.Json;
using SkolplattformenElevApi.Models;
using System.Text.Json.Serialization;
using SkolplattformenElevApi.Models.Internal.Kalendarium;
using SkolplattformenElevApi.Utils;

namespace SkolplattformenElevApi;

public partial class Api
{
    public async Task<List<KalendariumItem>> GetKalendariumAsync()
    {
        // https://elevstockholm.sharepoint.com/sites/<skola>/_api/web/lists/getByTitle('Matsedel')/items


        if (string.IsNullOrEmpty(_schoolSharepointUrl))
        {
            var user = await this.GetUserAsync();
            var url = user?.Schools?.FirstOrDefault(s => s.ExternalId == user.PrimarySchoolGuid)?.Url;
            _schoolSharepointUrl = url;
        }

        var items = new List<KalendariumItem>();

        if (!string.IsNullOrEmpty(_schoolSharepointUrl))
        {
            var url = $"{_schoolSharepointUrl}/_api/web/lists/getByTitle('Kalendarium')/items";
            //var responseMessage = await _httpClient.GetAsync(url);
            //var content = await responseMessage.Content.ReadAsStringAsync();

            var readNext = true;

            while (readNext)
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Get,
                    Headers =
                    {
                        { "Accept", "application/json" },
                    }
                };

                request.Headers.TryAddWithoutValidation("accept", new[] { "application/json;odata=nometadata" });
                var responseMessage = await _httpClient.SendAsync(request);
                var content = await responseMessage.Content.ReadAsStringAsync();


                // TODO: Find out the format of the response and return a List<Meal>

                var deserialized = JsonSerializer.Deserialize<KalendariumResponse>(content);


                foreach (var item in deserialized.Value.Where(x => x.EventDate > DateTime.Now.AddDays(-10)))
                {

                    var kalendeItem = new KalendariumItem
                    {
                        // Dates seems to be in UTC, and should be converted unless it's all day event
                        Title = item.Title,
                        Description = item.Description,
                        IsAllDayEvent = item.FAllDayEvent
                    };

                    try
                    {
                        kalendeItem.Created = item.Created.ToCEST();
                        kalendeItem.StartDate = item.FAllDayEvent ? item.EventDate : item.EventDate.ToCEST();
                        kalendeItem.EndDate = item.FAllDayEvent ? item.EndDate : item.EndDate.ToCEST();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    items.Add(kalendeItem);
                }

                if (string.IsNullOrEmpty(deserialized.OdataNextLink))
                {
                    readNext = false;
                }
                else
                {
                    url = deserialized.OdataNextLink;
                }
            }
        }


        return items;
    }


}

