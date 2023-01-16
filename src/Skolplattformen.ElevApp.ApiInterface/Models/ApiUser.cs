namespace Skolplattformen.ElevApp.ApiInterface.Models
{
    

    public class ApiUser
    {
        public Guid Id { get; set; }
        public Guid ExternalId { get; set; }
      //  public string Firstname { get; set; }
        //public string Lastname { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PrimarySchoolName { get; set; } = string.Empty;
        public Guid PrimarySchoolGuid { get; set; }
        public List<School> Schools { get; set; } = new List<School>();
    }
}
