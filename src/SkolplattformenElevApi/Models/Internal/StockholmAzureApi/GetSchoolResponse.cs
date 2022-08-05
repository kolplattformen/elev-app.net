namespace SkolplattformenElevApi.Models.Internal.StockholmAzureApi
{
    internal class SchoolDetailsData
    {
        public string ExternalId { get; set; }
        public string SchoolName { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string Locality { get; set; }
        public string VisitingAddress { get; set; }
        public string Email { get; set; }
        public SchoolPrincipal Principal { get; set; }
    }

    internal class SchoolPrincipal
    {
        public string Fullname { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
    }

    internal class GetSchoolResponse
    {
        public bool Success { get; set; }
        public object Error { get; set; }
        public SchoolDetailsData Data { get; set; }
    }

}
