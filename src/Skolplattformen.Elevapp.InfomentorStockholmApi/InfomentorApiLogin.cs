using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Skolplattformen.ElevApp.ApiInterface;

namespace Skolplattformen.Elevapp.InfomentorStockholmApi
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
        }
    }

    public class LoginCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
