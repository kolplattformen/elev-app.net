using SkolplattformenElevApi.Models;

namespace SkolplattformenElevApi;

public partial class Api
{
    public async Task<List<Meal>> GetMealsAsync(int year, int week)
    {
        // https://elevstockholm.sharepoint.com/sites/<skola>/_api/web/lists/getByTitle('Matsedel')/items

        var part = ApiPart.GetMeals;

        if (string.IsNullOrEmpty(_schoolSharepointUrl))
        {
            var user = await this.GetUserAsync();
            var url = user?.Schools?.FirstOrDefault(s => s.ExternalId == user.PrimarySchoolGuid)?.Url;
            _schoolSharepointUrl = url;
        }

        if (!string.IsNullOrEmpty(_schoolSharepointUrl))
        {
            var url = $"{_schoolSharepointUrl}/_api/web/lists/getByTitle('Matsedel')/items?as=json";
            //var responseMessage = await _httpClient.GetAsync(url);
            //var content = await responseMessage.Content.ReadAsStringAsync();

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

        }

        UpdateStatus(part, ApiReadSuccessIndicator.Unknown);
        return new List<Meal>();
    }
}

