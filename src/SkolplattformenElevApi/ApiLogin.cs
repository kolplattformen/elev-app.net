using System.Net;
using System.Text;
using System.Text.Json;
using System.Web;
using SkolplattformenElevApi.Models.Internal.Login;
using SkolplattformenElevApi.Models.Internal.Sharepoint;

namespace SkolplattformenElevApi
{
    public partial class Api
    {
        public async Task LogInAsync(string email, string username, string password)
        {

            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var temp_url = "https://skolplattformen.stockholm.se/";
            var temp_res = await _httpClient.GetAsync(temp_url);

            var temp_host = "https://elevstockholm.sharepoint.com";
            while (temp_res.Headers.Location != null)
            {
                temp_url = temp_res.Headers.Location?.ToString();

                if (temp_url.StartsWith("/"))
                {
                    temp_url = temp_host + temp_url;
                }

                temp_res = await _httpClient.GetAsync(temp_url);
            }


            // temp_url = "https://login.microsoftonline.com/e36726e9-4d94-4a77-be61-d4597f4acd02/oauth2/authorize?client_id=00000003-0000-0ff1-ce00-000000000000&response_mode=form_post&protectedtoken=true&response_type=code%20id_token&resource=00000003-0000-0ff1-ce00-000000000000&scope=openid&nonce=D183A6ABC1D1AC7C936304BB86DF8DAC910B49EBC59F2480-DAC0A994AB24A884A2F5B50818B527241CDB7D3431E47CA861C947015C5C1F6F&redirect_uri=https%3A%2F%2Felevstockholm.sharepoint.com%2F_forms%2Fdefault.aspx&state=OD0w&claims=%7B%22id_token%22%3A%7B%22xms_cc%22%3A%7B%22values%22%3A%5B%22CP1%22%5D%7D%7D%7D&wsucxt=1&cobrandid=11bd8083-87e0-41b5-bb78-0bc43c8a8e8a&client-request-id=7a323fa0-c071-4000-4218-a4696e60d3c2&sso_reload=true";
            temp_url = $"{temp_url}&sso_reload=true";

            _cookieContainer.Add(new Cookie("AADSSO", "NA|NoExtension", "/", "login.microsoftonline.com"));
            _cookieContainer.Add(new Cookie("SSOCOOKIEPULLED", "1", "/", "login.microsoftonline.com"));

            //SSOCOOKIEPULLED=1
            temp_res = await _httpClient.GetAsync(temp_url);
            var temp_content = await temp_res.Content.ReadAsStringAsync();
            var json = RegExp("\\$Config=(.*});", temp_content);
            var authorizeResponse = JsonSerializer.Deserialize<AuthorizeResponse>(json, jsonSerializerOptions);

            temp_url = "https://login.live.com/Me.htm?v=3";
            temp_res = await _httpClient.GetAsync(temp_url);


            //TODO what is flowToken and OriginalRequest? FlowToken is sFT in the result of line 43 (sso_reload=true). OriginalRequest is sCtx. Regex "\$Config=(.*});"gm 
            // todo: Anropet innehåller en hel del headers. Kolla var de kommer ifrån. Verkar inte behövas
            var content =
                "{\"username\":\"" + email + "\",\"isOtherIdpSupported\":true,\"checkPhones\":false,\"isRemoteNGCSupported\":true,\"isCookieBannerShown\":false,\"isFidoSupported\":true,\"originalRequest\":\"" + authorizeResponse.sCtx + "\",\"country\":\"SE\",\"forceotclogin\":false,\"isExternalFederationDisallowed\":false,\"isRemoteConnectSupported\":false,\"federationFlags\":0,\"isSignup\":false,\"flowToken\":\"" + authorizeResponse.sFT + "\",\"isAccessPassSupported\":true}";

            temp_url = "https://login.microsoftonline.com/common/GetCredentialType?mkt=en-US";
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(temp_url),
                Method = HttpMethod.Post,
                Headers =
            {
                {"AADSSO", "NA|NoExtension" },
                {"brcap", "0" },
                {"clrc","{%2219132%22%3a[%22P4dbKyr2%22%2c%2299lxtQhC%22]}"},
                {"hpgrequestid", authorizeResponse.sessionId },
                {"canary", authorizeResponse.apiCanary},
                {"hpgid", authorizeResponse.hpgid.ToString()},
                {"hpgact", authorizeResponse.hpgact.ToString()},
                {"client-request-id", authorizeResponse.correlationId}
            },
                Content = new StringContent(content)
            };
            temp_res = await _httpClient.SendAsync(request);

            //temp_res = await _httpClient.PostAsync(temp_url, new StringContent(content));
            var temp = await temp_res.Content.ReadAsStringAsync();
            var credType = JsonSerializer.Deserialize<CredentialType>(temp);


            temp_url = credType.Credentials.FederationRedirectUrl;
            temp_url = temp_url.Replace("wctx=", "wctx=LoginOptions%3D3%26") + "&cbcxt=";
            temp_res = await _httpClient.GetAsync(temp_url);
            temp_content = await temp_res.Content.ReadAsStringAsync();

            var samlRequest = RegExp("\"SAMLRequest\\\" value=\\\"([^\\\"]*)\"", temp_content);
            var relayState = RegExp("\"RelayState\\\" value=\\\"([^\\\"]*)\"", temp_content);
            temp_url = RegExp("action=\\\"([^\\\"]*)", temp_content);

            temp_res = await _httpClient.PostAsync(temp_url, new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("SAMLRequest", samlRequest),
            new KeyValuePair<string, string>("RelayState", relayState)
        }));



            temp_url = temp_res.Headers.Location?.ToString();
            var samlTransactionId = new Uri(temp_url).Query.Split("&").First(x => x.StartsWith("SAMLTRANSACTIONID=")).Split("=")[1];
            temp_res = await _httpClient.GetAsync(temp_url);



            temp_url = temp_res.Headers.Location?.ToString();
            temp_res = await _httpClient.GetAsync(temp_url);

            //TODO: Parametern startpage är Base64encodad och innehåller SAMLTRANSACTIONID. Måste göras om där den finns.

            // Byt till Elevinloggning. Behövs?
            temp_url = $"https://login001.stockholm.se/siteminderagent/forms/aelever.jsp?SMAUTHREASON=0&SMAGENTNAME=IfNE0iMOtzq2TcxFADHylR6rkmFtwzoxRKh5nRMO9NBqIxHrc38jFyt56FASdxk1&SMQUERYDATA=null&TARGET=https://login001.stockholm.se/affwebservices/redirectjsp/eduadfs.jsp?SMPORTALURL=https%3A%2F%2Flogin001.stockholm.se%2Faffwebservices%2Fpublic%2Fsaml2sso&SAMLTRANSACTIONID={samlTransactionId}";
            temp_res = await _httpClient.GetAsync(temp_url);


            //Ladda sida för inloggning med användnamn och lösen
            temp_url = UpdateSamlTransactionIdInEncodedStartpageParam($"https://login001.stockholm.se/siteminderagent/forms/loginForm.jsp?SMAGENTNAME=IfNE0iMOtzq2TcxFADHylR6rkmFtwzoxRKh5nRMO9NBqIxHrc38jFyt56FASdxk1&POSTTARGET=https://login001.stockholm.se/NECSelev/form/b64startpage.jsp?startpage=aHR0cHM6Ly9sb2dpbjAwMS5zdG9ja2hvbG0uc2UvYWZmd2Vic2VydmljZXMvcmVkaXJlY3Rqc3AvZWR1YWRmcy5qc3A/U01QT1JUQUxVUkw9aHR0cHMlM0ElMkYlMkZsb2dpbjAwMS5zdG9ja2hvbG0uc2UlMkZhZmZ3ZWJzZXJ2aWNlcyUyRnB1YmxpYyUyRnNhbWwyc3NvJlNBTUxUUkFOU0FDVElPTklEPTEwMjkwNThjLTM0MTg2YWFhLTRmYWRiYzY5LTRkN2RkMDU3LTczYmRkNGQ3LTA1MA==&TARGET=https://login001.stockholm.se/affwebservices/redirectjsp/eduadfs.jsp?SMPORTALURL=https%3A%2F%2Flogin001.stockholm.se%2Faffwebservices%2Fpublic%2Fsaml2sso&SAMLTRANSACTIONID={samlTransactionId}", samlTransactionId);
            temp_res = await _httpClient.GetAsync(temp_url);
            temp_content = await temp_res.Content.ReadAsStringAsync();
            var tttttt = RegExp("name=target value='([^']*)'", temp_content);

            // Här skickas inloggningsuppgifterna in
            temp_url = "https://login001.stockholm.se/siteminderagent/forms/login.fcc";
            temp_res = await _httpClient.PostAsync(temp_url, new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("user", username),
            new KeyValuePair<string, string>("password", password),
            new KeyValuePair<string, string>("SMENC", "ISO-8859-1"),
            new KeyValuePair<string, string>("SMLOCALE", "US-EN"),
            new KeyValuePair<string, string>("target", UpdateSamlTransactionIdInEncodedStartpageParam("https://login001.stockholm.se/NECSelev/form/b64startpage.jsp?startpage=aHR0cHM6Ly9sb2dpbjAwMS5zdG9ja2hvbG0uc2UvYWZmd2Vic2VydmljZXMvcmVkaXJlY3Rqc3AvZWR1YWRmcy5qc3A/U01QT1JUQUxVUkw9aHR0cHMlM0ElMkYlMkZsb2dpbjAwMS5zdG9ja2hvbG0uc2UlMkZhZmZ3ZWJzZXJ2aWNlcyUyRnB1YmxpYyUyRnNhbWwyc3NvJlNBTUxUUkFOU0FDVElPTklEPTEwMjkwNThjLTM0MTg2YWFhLTRmYWRiYzY5LTRkN2RkMDU3LTczYmRkNGQ3LTA1MA==", samlTransactionId)),
            new KeyValuePair<string, string>("smauthreason", "null"),
            new KeyValuePair<string, string>("smagentname", "IfNE0iMOtzq2TcxFADHylR6rkmFtwzoxRKh5nRMO9NBqIxHrc38jFyt56FASdxk1"),
            new KeyValuePair<string, string>("smquerydata", "null"),
            new KeyValuePair<string, string>("postpreservationdata", "null"),
            new KeyValuePair<string, string>("submit", "")
        }));


            while (temp_res.Headers.Location != null)
            {
                temp_url = temp_res.Headers.Location?.ToString();
                temp_res = await _httpClient.GetAsync(temp_url);
            }

            temp_content = await temp_res.Content.ReadAsStringAsync();
            var samlResponse = RegExp("\"SAMLResponse\\\" value=\\\"([^\\\"]*)\"", temp_content);
            relayState = RegExp("\"RelayState\\\" value=\\\"([^\\\"]*)\"", temp_content);

            // Har hämtat rad 72 och hämtat variablerna

            temp_url = "https://fs.edu.stockholm.se/adfs/ls/";
            temp_res = await _httpClient.PostAsync(temp_url, new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("SAMLResponse", samlResponse),
            new KeyValuePair<string, string>("RelayState", relayState)
        }));
            temp_content = await temp_res.Content.ReadAsStringAsync();

            var wa = RegExp("\"wa\\\" value=\\\"([^\\\"]*)\"", temp_content);
            var wresult = HttpUtility.HtmlDecode(RegExp("\"wresult\\\" value=\\\"([^\\\"]*)\"", temp_content));
            var wctx = HttpUtility.HtmlDecode(RegExp("\"wctx\\\" value=\\\"([^\\\"]*)\"", temp_content));

            // https://login.microsoftonline.com/login.srf
            _cookieContainer.Add(new Cookie("ESTSWCTXFLOWTOKEN", credType.FlowToken, "/", "login.microsoftonline.com"));
            temp_url = RegExp("action=\\\"([^\\\"]*)", temp_content);
            temp_res = await _httpClient.PostAsync(temp_url, new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("wa", wa),
            new KeyValuePair<string, string>("wresult", wresult),
            new KeyValuePair<string, string>("wctx", wctx)
        }));

            // Skickat data till login.srf på rad 76

            temp_content = await temp_res.Content.ReadAsStringAsync();
            json = RegExp("\\$Config=(.*});", temp_content);
            var loginSrfAnswer = JsonSerializer.Deserialize<LoginSrfAnswer>(json, jsonSerializerOptions);

            _cookieContainer.Add(new Cookie("ESTSWCTXFLOWTOKEN", null, "/", "login.microsoftonline.com"));

            temp_url = "https://login.microsoftonline.com/kmsi";
            temp_res = await _httpClient.PostAsync(temp_url, new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("LoginOptions", "3"),
            new KeyValuePair<string, string>("type", "28"),
            new KeyValuePair<string, string>("ctx", loginSrfAnswer.sCtx),
            new KeyValuePair<string, string>("hpgrequestid", loginSrfAnswer.sessionId),
            new KeyValuePair<string, string>("flowToken", loginSrfAnswer.sFT),
            new KeyValuePair<string, string>("canary", loginSrfAnswer.canary),
            new KeyValuePair<string, string>("i19", "23"),
        }));

            temp_content = await temp_res.Content.ReadAsStringAsync();

            var code = RegExp("\"code\\\" value=\\\"([^\\\"]*)\"", temp_content);
            var id_token = HttpUtility.HtmlDecode(RegExp("\"id_token\\\" value=\\\"([^\\\"]*)\"", temp_content));
            var state = RegExp("\"state\\\" value=\\\"([^\\\"]*)\"", temp_content);

            var sessionState = RegExp("\"session_state\\\" value=\\\"([^\\\"]*)\"", temp_content);
            var correlationId = RegExp("\"correlation_id\\\" value=\\\"([^\\\"]*)\"", temp_content);
            temp_url = RegExp("action=\\\"([^\\\"]*)", temp_content);

            temp_res = await _httpClient.PostAsync(temp_url, new FormUrlEncodedContent(new[]
              {
            new KeyValuePair<string, string>("code", code),
            new KeyValuePair<string, string>("id_token", id_token),
            new KeyValuePair<string, string>("state", state),
            new KeyValuePair<string, string>("session_state", sessionState),
            new KeyValuePair<string, string>("correlation_id", correlationId),

        }));
            temp_content = await temp_res.Content.ReadAsStringAsync();

            while (temp_res.Headers.Location != null)
            {
                temp_url = "https://elevstockholm.sharepoint.com" + temp_res.Headers.Location?.ToString();
                temp_res = await _httpClient.GetAsync(temp_url);
            }

            // DONE at last. The last redirect should be back to the startpage but now logged in.

            var url = "https://elevstockholm.sharepoint.com/sites/skolplattformen/";

            var res = await _httpClient.GetAsync(url);

            await SetupAfterLoginAsync(res);

            _email = email;
            

            // Tests below this line -------------------------------------------------------------------------------------------



            //temp_url = "";
            //temp_res = await _httpClient.GetAsync(temp_url);

        }


        public async Task RefreshLoginAsync()
        {
            var url = "https://elevstockholm.sharepoint.com/sites/skolplattformen/";

            var res = await _httpClient.GetAsync(url);

            await SetupAfterLoginAsync(res);

        }

        private async Task SetupAfterLoginAsync(HttpResponseMessage responseMessage)
        {
            var content = await responseMessage.Content.ReadAsStringAsync();

            _formDigestValue = RegExp("formDigestValue\":\"([^\\\"]*)\"", content);
            _spfx3rdPartyServicePrincipalId = RegExp("spfx3rdPartyServicePrincipalId\":\"([^\\\"]*)\"", content);

            //TODO: Try find a better way to get this, maybe get and deserialize the entire object to get this and the above values
            _apiEndpoint = RegExp("appId\\\\\\\\\\\":\\\\\\\\\\\"([^\\\\]*)\\\\", content);

            if (responseMessage.Headers.TryGetValues("SPRequestGuid", out var spHeader))
            {
                _sharePointRequestGuid = spHeader.First();
            }

            _cookieContainer.Add(new Cookie("KillSwitchOverrides_enableKillSwitches", "", "/", "sharepoint.com"));
            _cookieContainer.Add(new Cookie("KillSwitchOverrides_disableKillSwitches", "", "/", "sharepoint.com"));


            try
            {
                await AbsenceSsoLoginAsync();
                await TimetableSsoLoginAsync();
            }
            catch
            {

            }
        }

        private string UpdateSamlTransactionIdInEncodedStartpageParam(string url, string samlTransationId)
        {
            // var startpage = new Uri(url).Query.Split("&").First(x => x.StartsWith("startpage=")).Split("=")[1];
            // var startpage = url.Substring(url.IndexOf('?')+1).Split("&").First(x => x.StartsWith("startpage=")).Split("=")[1];
            var startpage = RegExp("startpage=([^&]*)", url);
            var decodedStartpage = Encoding.UTF8.GetString(Convert.FromBase64String(startpage));
            var oldSamlTransactionId = new Uri(decodedStartpage).Query.Split("&").First(x => x.StartsWith("SAMLTRANSACTIONID=")).Split("=")[1];
            var newStartpage = decodedStartpage.Replace(oldSamlTransactionId, samlTransationId);
            var newEncodedStartpage = Convert.ToBase64String(Encoding.UTF8.GetBytes(newStartpage));

            var newUrl = url.Replace(startpage, newEncodedStartpage);
            return newUrl;
        }

    }
}
