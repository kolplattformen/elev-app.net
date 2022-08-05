namespace SkolplattformenElevApi.Models.Internal.Login;

internal class AuthorizeResponse
{
    public string sFT { get; set; }
    public string sCtx { get; set; }
    public string apiCanary { get; set; }
    public string correlationId { get; set; }
    public int hpgact { get; set; }
    public int hpgid { get; set; }
    public string sessionId { get; set; }
}