using System.Net.Http.Headers;
using System.Text.Json;
using System.Web;
using SkolplattformenElevApi.Models;
using SkolplattformenElevApi.Models.Internal.Calendar;

namespace SkolplattformenElevApi;

public partial class Api
{

    private async Task<string> GetAzureApiAccessTokenAsync()
    {

        if (!string.IsNullOrEmpty(_apiEndpointAccessToken))
        {
            return _apiEndpointAccessToken;
        }


        /*
https://login.microsoftonline.com/e36726e9-4d94-4a77-be61-d4597f4acd02/oauth2/v2.0/authorize
?response_type=id_token%20token
&scope=be5acbda-1fcc-41f9-b401-7838560e55f9%2F.default%20openid%20profile // is in response to https://elevstockholm.sharepoint.com/sites/skolplattformen/
&client_id=fed4f3c2-f0b7-4bab-b5a0-cd91fe313482  //is in response to https://elevstockholm.sharepoint.com/sites/skolplattformen/  "spfx3rdPartyServicePrincipalId":"fed4f3c2-f0b7-4bab-b5a0-cd91fe313482"
&redirect_uri=https%3A%2F%2Felevstockholm.sharepoint.com%2F_forms%2Fspfxsinglesignon.aspx
&state=eyJpZCI6ImI1ZTcyMzMyLWI5MDEtNGU4My04MDQ4LTFmNDQ1ZmRjZWY3OSIsInRzIjoxNjUzMDQyMDg3LCJtZXRob2QiOiJyZWRpcmVjdEludGVyYWN0aW9uIn0%3D%7Chttps%3A%2F%2Felevstockholm.sharepoint.com%2Fsites%2Fskolplattformen%2F // where is it?
&nonce=55483b5e-2c0d-4391-b6ad-5e74f5503c33 // where is it?
&client_info=1
&x-client-SKU=MSAL.JS
&x-client-Ver=1.4.12
&login_hint=<redacted>%40elevmail.stockholm.se
&client-request-id=922d556e-1619-477c-806d-7a250af73823 // where is it?
&response_mode=fragment
         */

        

        // try random guids and see what happens
        var nonce = Guid.NewGuid().ToString().ToLower();
        var clientRequestId = Guid.NewGuid().ToString().ToLower();
        var scope = HttpUtility.UrlEncode( $"{_apiEndpoint}/.default openid profile");
        var ts = (Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        var blaj = "{\"id\":\"" + Guid.NewGuid().ToString().ToLower() +  "\",\"ts\":" + ts + ",\"method\":\"redirectInteraction\"}";
        var state = HttpUtility.UrlEncode($"{Base64Encode(blaj)}|https://elevstockholm.sharepoint.com/sites/skolplattformen/");
        var loginHint = HttpUtility.UrlEncode(_email);

        var url = "https://login.microsoftonline.com/e36726e9-4d94-4a77-be61-d4597f4acd02/oauth2/v2.0/authorize"
                  + "?response_type=id_token%20token"
                  + $"&scope={scope}" // is in response to https://elevstockholm.sharepoint.com/sites/skolplattformen/
                  + $"&client_id={_spfx3rdPartyServicePrincipalId}" //is in response to https://elevstockholm.sharepoint.com/sites/skolplattformen/  "spfx3rdPartyServicePrincipalId":
                  + "&redirect_uri=https%3A%2F%2Felevstockholm.sharepoint.com%2F_forms%2Fspfxsinglesignon.aspx"
                  + $"&state={state}" // where is it?
                  + $"&nonce={nonce}" // where is it?
                  + "&client_info=1"
                  + "&x-client-SKU=MSAL.JS"
                  + "&x-client-Ver=1.4.12"
                  + $"&login_hint={loginHint}"
                  + $"&client-request-id={clientRequestId}"
                  + "&response_mode=fragment";

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(url),
            Method = HttpMethod.Get,
            Headers =
            {
                { "Referer", "https://elevstockholm.sharepoint.com/" },
                { "Origin", "https://elevstockholm.sharepoint.com" },
            },
        };
        var response = await _httpClient.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        var accessToken = RegExp("access_token=([^\\&]*)\\&", content);

        _apiEndpointAccessToken = accessToken;

        return accessToken;
    }


    public async Task<List<CalendarItem>> GetCalendarAsync(DateOnly date)
    {

        var token = await GetAzureApiAccessTokenAsync();

        var url =
            $"https://stockholm-o365-api.azurewebsites.net//api/Calender/calender?date={date.ToString("yyyy-MM-dd")}";

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(url),
            Method = HttpMethod.Get,
            Headers =
            {
                { "Referer", "https://elevstockholm.sharepoint.com/" },
                { "Accept", "application/json"},
                { "Origin", "https://elevstockholm.sharepoint.com" },
            },
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.SendAsync(request);


   
        var content = await response.Content.ReadAsStringAsync();
        var deserialized = JsonSerializer.Deserialize<CalendarResponse>(content);

        if (deserialized?.Data == null)
        {
            return new List<CalendarItem>();
        }

        var calendarItemList = new List<CalendarItem>();
        foreach (var item in deserialized.Data)
        {
            calendarItemList.Add(new CalendarItem
            {
                Id = item.Id,
                Title = item.Title,
                Location = item.Location,
                Start = item.Start,
                End = item.End,
                IsAllDay = false,
                WebLink = item.WebLink,
            });
        }

        // Whole day events gets wrong timezone?
        return calendarItemList; 
    }
    
}
