using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Skolplattformen.ElevApp.ApiInterface;

namespace Skolplattformen.ElevApp.InfomentorStockholmApi
{
    public partial class InfomentorApi :IApi
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private int userId = 0;

        private readonly CookieContainer _cookieContainer;
        private readonly HttpClient _httpClient;

        public InfomentorApi()
        {
            _cookieContainer = new CookieContainer();
            var httpClientHandler = new HttpClientHandler { CookieContainer = _cookieContainer, AllowAutoRedirect = false };
            _httpClient = new HttpClient(httpClientHandler);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36"); ;
        }

        public async Task LogInAsync(object loginCredentials)
        {
            if (loginCredentials is LoginCredentials lc)
            {
                _username = lc.Username;
                _password = lc.Password;
               
            }
            else { throw new Exception("Wrong login credentials"); }

            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var temp_url = "https://hub.infomentor.se/";
            var temp_res = await _httpClient.GetAsync(temp_url);

            var temp_host = "https://hub.infomentor.se/";
            while (temp_res.Headers.Location != null)
            {
                temp_url = temp_res.Headers.Location?.ToString();

                if (temp_url.StartsWith("/"))
                {
                    temp_url = temp_host + temp_url;
                }

                temp_res = await _httpClient.GetAsync(temp_url);
            }

            temp_url = "https://infomentor.se/swedish/production/mentor/";
            temp_res = await _httpClient.GetAsync(temp_url);

            string samlTransactionId = string.Empty;
            string samlRequest = string.Empty;
            string sigAlg = string.Empty;
            string signature = string.Empty;

            temp_url = "https://sso.infomentor.se/login.ashx?idp=stockholm_stu";
            temp_res = await _httpClient.GetAsync(temp_url);
            while (temp_res.Headers.Location != null)
            {
                var query = new Uri(temp_url).Query;
                if (query.StartsWith('?')) query = query[1..];
                //query = HttpUtility.UrlDecode(query);

                if (query.Contains("SAMLTRANSACTIONID"))
                {
                    samlTransactionId = query.Split("&").First(x => x.StartsWith("SAMLTRANSACTIONID=")).Split("=")[1];
                }
                if (temp_url.Contains("/public/saml2sso"))
                {
                    var splitted = query.Split("&");
                    samlRequest = splitted.First(x => x.StartsWith("SAMLRequest=")).Split("=")[1];
                    sigAlg = splitted.First(x => x.StartsWith("SigAlg=")).Split("=")[1];
                    signature = splitted.First(x => x.StartsWith("Signature=")).Split("=")[1];
                }
                temp_url = temp_res.Headers.Location?.ToString();

                if (temp_url.StartsWith("/"))
                {
                    temp_url = temp_host + temp_url;
                }

                temp_res = await _httpClient.GetAsync(temp_url);
            }
            var temp_content = await temp_res.Content.ReadAsStringAsync();
            var loginWithUserAndPass = RegExp("loginForm.jsp([^\"]*)", temp_content);
            temp_url = "https://login001.stockholm.se/siteminderagent/forms/loginForm.jsp" + WebUtility.HtmlDecode(loginWithUserAndPass);
            temp_url = "https://login001.stockholm.se/siteminderagent/forms/loginForm.jsp" + loginWithUserAndPass;

            //Ladda sida för inloggning med användnamn och lösen
            //temp_url = UpdateStartpageParams($"https://login001.stockholm.se/siteminderagent/forms/loginForm.jsp?SMAGENTNAME=IfNE0iMOtzq2TcxFADHylR6rkmFtwzoxRKh5nRMO9NBqIxHrc38jFyt56FASdxk1&POSTTARGET=https://login001.stockholm.se/NECSelev/form/b64startpage.jsp?startpage=aHR0cHM6Ly9sb2dpbjAwMS5zdG9ja2hvbG0uc2UvYWZmd2Vic2VydmljZXMvcmVkaXJlY3Rqc3AvZWxldi5qc3A/U0FNTFJlcXVlc3Q9bFpMTmJzSXdFSVR2bGZvT2tlJTJGRUNTU1FXQWtvS2hja0tsWFE5dEJMWmVKTll6V3hhZGFoUEg0ZCUyRnFVSzFGNjklMkI4Mk1kemVaYk92SzJVQ0RVcXVVJTJCSzVISnVQN3V5UnJUYWtXOE5VQ0dzZTJLR1MxU0VuYktLWTVTbVNLMTRETTVHeVpQYzVaMyUyRlZZRFlZTGJqaHhadE9VU0JFTWVCaDVxeUNFWEFTclFSem54V2dvb2lDSyUyQmhGdzRSSG45V2hyY1VzaHRqQlRhTGd5OXNuckJ6MHY2dm5oc3o5a1ljekNrZXY3bzZFZngyJTJGRW1kcFlVbkd6bzB0ajFzZ29yZlNIVko3bnUyaDAlMkZsbnFxbllSS0MlMkJLYjFnaE5CdVpBOUoxdTZwa1RwSFhWUjlSZDdaUEhGRnVJQ1VGcnhDSWs2SHQ3cVFmdE1LMmhtYTVoMThXODdPWlpWMnBDbDJETXJycGpIYjJMc2R5TzVGaW5aNUN2S05weVg2R3R3ZTRiclJsZEVYc0Fod24yYzJqJTJCUXZJajRISiUyQkhxODM2RVN1cmZZMjluUEN0bUo0RDh0TzlyeVdTc2txQndXZGpXTnpMdmFvWFJSdkpFdm9hZW1neUs5SnBuUWM5anVXT25sdFk1JTJGQUElM0QlM0QmU2lnQWxnPWh0dHAlM0ElMkYlMkZ3d3cudzMub3JnJTJGMjAwMCUyRjA5JTJGeG1sZHNpZyUyM3JzYS1zaGExJlNpZ25hdHVyZT1SRm5DTTBOZk9oZnp3NzdkR2QlMkJWVGJ3NlVHSXFCU1lZdXR4Q2FpU21ZdFc0cmE3JTJGUjZJYTNMcmVjbE53dVVYcVpXdHNNcXd2VjF5VFJOWjY4MXNTOFI2Sm9FY0ZvdERoMzcyWnJ4WWg5Z2Y1JTJCTlVmNjhMaHhVJTJGQ05xNWc5SG5VMmNQZmpCVUpqTmFFaiUyRkZjSVdlUUNBMldranBNUVFrQmM0JTJGNHhxaXdXNkduY014cG1Eb3FBYTZQMGNqMSUyQnYlMkZwWHlTMGdITGQ3VkFCNWlRZFpjN3N4Tm1TQTFvM1M0JTJCeU12QmlIQklFdURodHR1VkZ6TG9BbUdXaCUyRkNjTndaUUxPJTJGeVhLNkxUQ0xUTHglMkY2Qk8yVUg1cW9YeW1kOVpVSVhJMCUyRldmTVZqbkR6eXZ1dWZwaEJ4UXpNSkFGREREUDh1dzFpbkpZb0lEN3h3UzYxSEpMeThCUSUzRCUzRCZTTVBPUlRBTFVSTD1odHRwcyUzQSUyRiUyRmxvZ2luMDAxLnN0b2NraG9sbS5zZSUyRmFmZndlYnNlcnZpY2VzJTJGcHVibGljJTJGc2FtbDJzc28mU0FNTFRSQU5TQUNUSU9OSUQ9M2NlY2E5ZDQtN2ZmYmYyZDYtODA2MmQ3OGUtN2VjODlhNTQtNDY0YWVmM2QtZWY=&TARGET=https://login001.stockholm.se/affwebservices/redirectjsp/elev.jsp?SAMLRequest={samlRequest}&SigAlg={sigAlg}&Signature={signature}&SMPORTALURL=https%3A%2F%2Flogin001.stockholm.se%2Faffwebservices%2Fpublic%2Fsaml2sso&SAMLTRANSACTIONID={samlTransactionId}",
            //    samlTransactionId, samlRequest, sigAlg, signature);
            temp_res = await _httpClient.GetAsync(temp_url);
            temp_content = await temp_res.Content.ReadAsStringAsync();


            // Här skickas inloggningsuppgifterna in

            var loginFccTargetPage
                = RegExp("target value='([^\']*)", temp_content);
            //loginFccTargetPage =  WebUtility.HtmlDecode(loginFccTargetPage);

            //var loginFccTargetPage =
            //    "https://login001.stockholm.se/NECSelev/form/b64startpage.jsp?startpage=aHR0cHM6Ly9sb2dpbjAwMS5zdG9ja2hvbG0uc2UvYWZmd2Vic2VydmljZXMvcmVkaXJlY3Rqc3AvZWxldi5qc3A/U0FNTFJlcXVlc3Q9bFpMTmJzSXdFSVR2bGZvT2tlJTJGRUNTU1FXQWtvS2hja0tsWFE5dEJMWmVKTll6V3hhZGFoUEg0ZCUyRnFVSzFGNjklMkI4Mk1kemVaYk92SzJVQ0RVcXVVJTJCSzVISnVQN3V5UnJUYWtXOE5VQ0dzZTJLR1MxU0VuYktLWTVTbVNLMTRETTVHeVpQYzVaMyUyRlZZRFlZTGJqaHhadE9VU0JFTWVCaDVxeUNFWEFTclFSem54V2dvb2lDSyUyQmhGdzRSSG45V2hyY1VzaHRqQlRhTGd5OXNuckJ6MHY2dm5oc3o5a1ljekNrZXY3bzZFZngyJTJGRW1kcFlVbkd6bzB0ajFzZ29yZlNIVko3bnUyaDAlMkZsbnFxbllSS0MlMkJLYjFnaE5CdVpBOUoxdTZwa1RwSFhWUjlSZDdaUEhGRnVJQ1VGcnhDSWs2SHQ3cVFmdE1LMmhtYTVoMThXODdPWlpWMnBDbDJETXJycGpIYjJMc2R5TzVGaW5aNUN2S05weVg2R3R3ZTRiclJsZEVYc0Fod24yYzJqJTJCUXZJajRISiUyQkhxODM2RVN1cmZZMjluUEN0bUo0RDh0TzlyeVdTc2txQndXZGpXTnpMdmFvWFJSdkpFdm9hZW1neUs5SnBuUWM5anVXT25sdFk1JTJGQUElM0QlM0QmU2lnQWxnPWh0dHAlM0ElMkYlMkZ3d3cudzMub3JnJTJGMjAwMCUyRjA5JTJGeG1sZHNpZyUyM3JzYS1zaGExJlNpZ25hdHVyZT1SRm5DTTBOZk9oZnp3NzdkR2QlMkJWVGJ3NlVHSXFCU1lZdXR4Q2FpU21ZdFc0cmE3JTJGUjZJYTNMcmVjbE53dVVYcVpXdHNNcXd2VjF5VFJOWjY4MXNTOFI2Sm9FY0ZvdERoMzcyWnJ4WWg5Z2Y1JTJCTlVmNjhMaHhVJTJGQ05xNWc5SG5VMmNQZmpCVUpqTmFFaiUyRkZjSVdlUUNBMldranBNUVFrQmM0JTJGNHhxaXdXNkduY014cG1Eb3FBYTZQMGNqMSUyQnYlMkZwWHlTMGdITGQ3VkFCNWlRZFpjN3N4Tm1TQTFvM1M0JTJCeU12QmlIQklFdURodHR1VkZ6TG9BbUdXaCUyRkNjTndaUUxPJTJGeVhLNkxUQ0xUTHglMkY2Qk8yVUg1cW9YeW1kOVpVSVhJMCUyRldmTVZqbkR6eXZ1dWZwaEJ4UXpNSkFGREREUDh1dzFpbkpZb0lEN3h3UzYxSEpMeThCUSUzRCUzRCZTTVBPUlRBTFVSTD1odHRwcyUzQSUyRiUyRmxvZ2luMDAxLnN0b2NraG9sbS5zZSUyRmFmZndlYnNlcnZpY2VzJTJGcHVibGljJTJGc2FtbDJzc28mU0FNTFRSQU5TQUNUSU9OSUQ9M2NlY2E5ZDQtN2ZmYmYyZDYtODA2MmQ3OGUtN2VjODlhNTQtNDY0YWVmM2QtZWY=";


            temp_url = "https://login001.stockholm.se/siteminderagent/forms/login.fcc";
            temp_res = await _httpClient.PostAsync(temp_url, new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("user", _username),
                new KeyValuePair<string, string>("password", _password),
                new KeyValuePair<string, string>("SMENC", "ISO-8859-1"),
                new KeyValuePair<string, string>("SMLOCALE", "US-EN"),
               // new KeyValuePair<string, string>("target", UpdateStartpageParams(loginFccTargetPage, samlTransactionId, samlRequest, sigAlg, signature)),
                new KeyValuePair<string, string>("target", loginFccTargetPage),
               new KeyValuePair<string, string>("smauthreason", "null"),
                new KeyValuePair<string, string>("smagentname", "IfNE0iMOtzq2TcxFADHylR6rkmFtwzoxRKh5nRMO9NBqIxHrc38jFyt56FASdxk1"),
                new KeyValuePair<string, string>("smquerydata", "null"),
                new KeyValuePair<string, string>("postpreservationdata", "null"),
                new KeyValuePair<string, string>("submit", "")
            }));

            var location = temp_res.Headers.Location?.ToString();
            if (!string.IsNullOrEmpty(location) && !location.Contains("b64startpage"))
            {
                throw new Exception("Login failed");
            }

            while (temp_res.Headers.Location != null)
            {
                temp_url = temp_res.Headers.Location?.ToString();

                if (temp_url.StartsWith("/"))
                {
                    temp_url = "https://login001.stockholm.se" + temp_url;
                }

                temp_res = await _httpClient.GetAsync(temp_url);
            }
            temp_content = await temp_res.Content.ReadAsStringAsync();
            var samlResponse = RegExp("\"SAMLResponse\\\" value=\\\"([^\\\"]*)\"", temp_content);

            temp_res = await _httpClient.PostAsync("https://sso.infomentor.se/login.ashx?idp=stockholm_stu", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("SAMLResponse", samlResponse),
                
            }));

            while (temp_res.Headers.Location != null)
            {
                temp_url = temp_res.Headers.Location?.ToString();

                if (temp_url.StartsWith("/"))
                {
                    temp_url = "https://sso.infomentor.se" + temp_url;
                }

                temp_res = await _httpClient.GetAsync(temp_url);
            }
            temp_content = await temp_res.Content.ReadAsStringAsync();
            var oauthToken = RegExp("\"oauth_token\\\" value=\\\"([^\\\"]*)\"", temp_content);

            temp_res = await _httpClient.PostAsync("https://infomentor.se/swedish/production/mentor/", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("oauth_token", oauthToken),

            }));

            while (temp_res.Headers.Location != null)
            {
                temp_url = temp_res.Headers.Location?.ToString();

                if (temp_url.StartsWith("/"))
                {
                    temp_url = "https://hub.infomentor.se/";
                }

                temp_res = await _httpClient.GetAsync(temp_url);
            }

            temp_res = await _httpClient.PostAsync($"https://hub.infomentor.se/authentication/authentication/isauthenticated/?_={DateTimeOffset.Now.ToUnixTimeMilliseconds()}", null);
            temp_content = await temp_res.Content.ReadAsStringAsync();

            
        }

        private string UpdateStartpageParams(string url, string samlTransationId, string samlRequest, string sigAlg, string signature)
        {

            var startpage = RegExp("startpage=([^&]*)", url);
            var decodedStartpage = Encoding.UTF8.GetString(Convert.FromBase64String(startpage));
            var queryParams = new Uri(decodedStartpage).Query[1..].Split("&");

            var oldSamlTransactionId = queryParams.FirstOrDefault(x => x.StartsWith("SAMLTRANSACTIONID="))?.Split("=")[1];
            var oldSamlReqest = queryParams.FirstOrDefault(x => x.StartsWith("SAMLRequest="))?.Split("=")[1];
            var oldSigAlg = queryParams.FirstOrDefault(x => x.StartsWith("SigAlg="))?.Split("=")[1];
            var oldSignature = queryParams.FirstOrDefault(x => x.StartsWith("Signature="))?.Split("=")[1];

            var newStartpage = decodedStartpage;
            if(oldSamlTransactionId != null) newStartpage = newStartpage.Replace(oldSamlTransactionId, samlTransationId);
            if(oldSamlReqest != null) newStartpage = newStartpage.Replace(oldSamlReqest, samlRequest);
            if(oldSigAlg != null) newStartpage = newStartpage.Replace(oldSigAlg, sigAlg);
            if(oldSignature != null) newStartpage = newStartpage.Replace(oldSignature, signature);
            var newEncodedStartpage = Convert.ToBase64String(Encoding.UTF8.GetBytes(newStartpage));

            var newUrl = url.Replace(startpage, newEncodedStartpage);
            return newUrl;
        }
        private string RegExp(string pattern, string source)
        {
            var reg = new Regex(pattern);
            var matches = reg.Matches(source);

            return matches[0].Groups[1].Value;
        }

    }


    public class LoginCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
