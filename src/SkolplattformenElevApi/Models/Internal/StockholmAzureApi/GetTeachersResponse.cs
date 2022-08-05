namespace SkolplattformenElevApi.Models.Internal.StockholmAzureApi
{
    internal class TeacherData
    {
        public int ID { get; set; }
        public string BATCH { get; set; }
        public string SIS_ID { get; set; }
        public string USERNAME { get; set; }
        public string SCHOOL_SIS_ID { get; set; }
        public string EMAILADDRESS { get; set; }
        public string STATUS { get; set; }
        public int ERRORCODE { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public int PRIVACY { get; set; }
        public bool ACTIVE { get; set; }
        public List<Section> Sections { get; set; }
    }

    internal class GetTeachersResponse
    {
        public bool Success { get; set; }
        public object Error { get; set; }
        public List<TeacherData> Data { get; set; }
    }

    internal class Section
    {
        public int ID { get; set; }
        public string BATCH { get; set; }
        public string SIS_ID { get; set; }
        public string SCHOOL_SIS_ID { get; set; }
        public string SECTION_NAME { get; set; }
        public string TERM_STARTDATE { get; set; }
        public string TERM_ENDDATE { get; set; }
        public string GROUPTYPE { get; set; }
        public object SUBJECTCODE { get; set; }
        public bool ACTIVE { get; set; }
    }

}
