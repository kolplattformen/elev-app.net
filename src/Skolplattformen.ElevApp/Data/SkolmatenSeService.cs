using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SkolplattformenElevApi.Models;
using SkolplattformenElevApi.Models.News;

namespace Skolplattformen.ElevApp.Data
{
    internal static class SkolmatenSeService
    {
        private static Dictionary<string, List<Meal>> _cache = new();

        public static async Task<List<Meal>> GetWeekAsync(string school, int year, int week)
        {
            var key = $"{year}_{week}";
            if (_cache.ContainsKey(key))
            {
                return _cache[key];
            }
            
            var limit = 1;
            var offset = 0;

            var today = DateTime.Today;
            var day = ISOWeek.ToDateTime(year, week, today.DayOfWeek);

            offset = (int)(day - today).TotalDays / 7;
            
            var meals = new List<Meal>();
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://skolmaten.se/{school}/rss/weeks/?limit={limit}&offset={offset}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var rssItems = ParseContent(content);

                    foreach (var item in rssItems)
                    {
                        meals.Add(new Meal
                        {
                            Date = DateTime.Parse(item.PublishDate),
                            Description = item.Description.Replace("<br/>", "\n"),
                            Title = item.Title
                        });
                    }
                }
            }

            _cache[key] = meals;
            return meals;
        }

        
        private static List<RssItem> ParseContent(string content)
        {
            var doc = XDocument.Parse(content);

            var entries = from item in doc.Root.Descendants()
                    .First(i => i.Name.LocalName == "channel")
                    .Elements()
                    .Where(i => i.Name.LocalName == "item")
                select new RssItem
                {
                    Title = item.Elements().First(i => i.Name.LocalName == "title").Value,
                    Description = item.Elements().First(i => i.Name.LocalName == "description").Value,
                    PublishDate = item.Elements().First(i => i.Name.LocalName == "pubDate").Value,

                };
            return entries.ToList();
        }

        public static async Task< (bool valid, string schoolName)> ValidateSchoolAsync(string school)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"https://skolmaten.se/{school}/rss/days/");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var doc = XDocument.Parse(content);
                        var schoolName = doc.Root.Descendants()
                            .First(i => i.Name.LocalName == "channel")
                            .Elements()
                            .First(i => i.Name.LocalName == "title").Value;
                        return (true, schoolName);
                    }
                }
            } 
            catch
            {
                return (false, null);
            }
            
            return (false, null);
        }
    }

    internal class RssItem{
        public string Title { get; set; }
        public string Description { get; set; }
        public string PublishDate { get; set; }
    }
}
