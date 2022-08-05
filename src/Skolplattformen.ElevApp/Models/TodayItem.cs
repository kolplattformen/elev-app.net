using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skolplattformen.ElevApp.Models
{
    public class TodayItem
    {
        public string StartTime { get; set; } = "00:00";
        public string EndTime { get; set; } = "00:00";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
    }
}
