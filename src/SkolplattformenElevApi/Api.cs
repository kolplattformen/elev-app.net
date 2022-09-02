using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace SkolplattformenElevApi;

public partial class Api:IApi
{
    private readonly CookieContainer _cookieContainer;
    private readonly HttpClient _httpClient;
    private string _sharePointRequestGuid;
    private string _formDigestValue;

    private string _email;
    private string? _schoolSharepointUrl;
    private string _spfx3rdPartyServicePrincipalId;
    private string _apiEndpoint;
    private string _apiEndpointAccessToken = string.Empty;

    private Dictionary<string, ApiReadSuccessIndicator> _statusDictionary = new Dictionary<string, ApiReadSuccessIndicator>();

    public Api()
    {
        _cookieContainer = new CookieContainer();
        var httpClientHandler = new HttpClientHandler { CookieContainer = _cookieContainer, AllowAutoRedirect = false };
        _httpClient = new HttpClient(httpClientHandler);
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36"); ;
    }


 
    private string RegExp(string pattern, string source)
    {
        var reg = new Regex(pattern);
        var matches = reg.Matches(source);
        
        return matches[0].Groups[1].Value;
    }

    private string Base64Encode(string input)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(input);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    private string Base64Decode(string input)
    {
        byte[] data = Convert.FromBase64String(input);
        return Encoding.UTF8.GetString(data);
    }

    private void UpdateStatus(string part, ApiReadSuccessIndicator successIndicator)
    {
        if (_statusDictionary.ContainsKey(part))
        {
            _statusDictionary[part] = successIndicator;
        }
        else
        {
            _statusDictionary.Add(part, successIndicator);
        }
    }

    public ApiReadSuccessIndicator GetStatus(string part)
    {
        if (_statusDictionary.ContainsKey(part))
        {
            return _statusDictionary[part];
        }
        else
        {
            return ApiReadSuccessIndicator.Unknown;
        }
    }

    public Dictionary<string, ApiReadSuccessIndicator> GetStatusAll()
    {
        return _statusDictionary;
    }
}

public enum ApiReadSuccessIndicator
{
    Unknown = 0,
    Success = 1,
    NoData = 2,
    Error = 3
}

public static class ApiPart
{
    public static string GetTeachers => "GetTeachers";
    public static string GetTimetable => "GetTimetable";
    public static string GetMeals => "GetMeals";
    public static string GetUser => "GetUser";
    public static string GetSchoolDetails => "GetSchoolDetails";
    public static string GetCalendar => "GetCalendar";
    public static string GetPlannedAbsence => "GetPlannedAbsence";
    public static string GetKalendarium => "GetKalendarium";
}
