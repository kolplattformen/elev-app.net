using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkolplattformenElevApi.Models.News
{
    public class NewsItem
    {
        public string Title { get; set; }
        public string InnerHtml { get; set; }
        public string Description { get; internal set; }
        public DateTime Published { get; internal set; }
        public DateTime Modified { get; internal set; }
        public string BannerImage { get; internal set; }
        public string AuthorName { get; internal set; }
    }
}
