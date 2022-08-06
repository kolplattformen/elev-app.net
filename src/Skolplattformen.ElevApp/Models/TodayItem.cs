using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skolplattformen.ElevApp.Models
{
    public enum TodatItemMark
    {
        Standard = 0,
        Warning = 1,
        AllDay = 2
    }
    public class TodayItem
    {
        public string StartTime { get; set; } = "00:00";
        public string EndTime { get; set; } = "00:00";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public TodatItemMark Mark { get; set; } = TodatItemMark.Standard;

        public Color Color => Mark switch
        {
            TodatItemMark.AllDay => Microsoft.Maui.Graphics.Color.FromRgb(255, 0, 0),
            TodatItemMark.Warning => Microsoft.Maui.Graphics.Color.FromRgb(255, 255, 0),
            _ => Microsoft.Maui.Graphics.Color.FromRgb(255, 255, 255)
        };
    }
}
