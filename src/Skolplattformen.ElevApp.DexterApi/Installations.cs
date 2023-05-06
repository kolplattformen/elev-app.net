namespace Skolplattformen.ElevApp.DexterApi;

public record Installation(string Id, string Name, string BaseUrl);
    
internal static class Installations
{
    private static readonly List<Installation> _installations = new List<Installation>
    {
        new Installation("KRAMFORS", "Kramfors", "https://kramfors.dexter-ist.com/Kramfors/services/resources/"),
        
        // You can add your school/district here by copying the line above and changing the values.
        
    };

    public static Installation? Get(string id)
    {
        return _installations.FirstOrDefault(x => x.Id == id);
    }
    public static List<Installation> GetAll()
    {
        return _installations;
    }


}