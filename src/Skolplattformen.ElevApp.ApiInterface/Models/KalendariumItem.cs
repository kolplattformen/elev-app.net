namespace Skolplattformen.ElevApp.ApiInterface.Models
{
    public class KalendariumItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsAllDayEvent { get; set; }
    }
}
