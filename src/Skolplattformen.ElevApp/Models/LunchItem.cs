namespace Skolplattformen.ElevApp.Models
{
    public class LunchItem
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public bool IsToday => Date.Date == DateTime.Now.Date;
    }
}
