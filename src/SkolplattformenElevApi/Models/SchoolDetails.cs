namespace SkolplattformenElevApi.Models;

public class SchoolDetails
{
    public Guid Id { get; set; }
    public Guid ExternalId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
       
    public string PostalCode { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Locality { get; set; }
    public string VisitingAddress { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PrincipalName { get; set; } = string.Empty;
    public string PrincipalPhone { get; set; } = string.Empty;
    public string PrincipalEmail { get; set; } = string.Empty;
}