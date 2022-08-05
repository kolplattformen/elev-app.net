namespace SkolplattformenElevApi.Models.Internal.StockholmAzureApi
{
    internal class UserData
    {
        public string Id { get; set; }
        public string PrimarySchool { get; set; }
        public string Name { get; set; }
        public string SchoolType { get; set; }
        public string UserType { get; set; }
        public string ExternalId { get; set; }
        public string UPN { get; set; }
        public List<UserSchoolData> Schools { get; set; }
    }

    internal class GetUserResponse
    {
        public bool Success { get; set; }
        public object Error { get; set; }
        public UserData Data { get; set; }
    }

    internal class UserSchoolData
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string ExternalId { get; set; }
        public string Url { get; set; }
        public object MergeSchoolName { get; set; }
        public object MergeSchoolExternalId { get; set; }
    }

}
