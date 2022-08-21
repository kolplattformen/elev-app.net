using System.Net.Http.Headers;
using System.Text.Json;
using SkolplattformenElevApi.Models;
using System.Text.Json.Serialization;
using SkolplattformenElevApi.Models.Internal.Kalendarium;

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


                foreach (var item in deserialized.Value)
                {
                    items.Add(new KalendariumItem
                    {
                        Title = item.Title,
                        Description = item.Description,
                        Created = item.Created,
                        StartDate = item.EventDate,
                        EndDate = item.EndDate,
                        IsAllDayEvent = item.FAllDayEvent
                    });
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


        return items.Where(i => i.StartDate.Date > DateTime.Now.AddDays(-10).Date).ToList();
    }


}

