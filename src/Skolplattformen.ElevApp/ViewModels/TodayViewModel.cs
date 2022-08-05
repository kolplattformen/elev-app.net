using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Skolplattformen.ElevApp.Data;
using Skolplattformen.ElevApp.Models;
using SkolplattformenElevApi.Models;


namespace Skolplattformen.ElevApp.ViewModels
{
    public partial class TodayViewModel: ObservableObject
    {
        private readonly SkolplattformenService _skolplattformenService;

        [ObservableProperty] private DateTime currentDate = DateTime.Now;
        [ObservableProperty] private string title = "";
        [ObservableProperty] private ObservableCollection<TodayItem> items;
        [ObservableProperty] private string schoolStartTime  = "";
        [ObservableProperty] private string schoolEndTime  = "";
        [ObservableProperty] public bool isSportsbagDay = false;
        


        public TodayViewModel(SkolplattformenService skolplattformenService)
        {
            _skolplattformenService = skolplattformenService;

            items = new ObservableCollection<TodayItem>();

            Task.Run(LoadData);
        }

        public async Task LoadData()
        {
            title = (currentDate.Date == DateTime.Now.Date)
                ? "Idag"
                : $"Min dag {currentDate.ToString("dd/MM")}";
            
            var todayItems = new List<TodayItem>();

            // get lessons

            var timetable = await _skolplattformenService.GetTimetableAsync(currentDate);
            foreach (var lesson in timetable)
            {
                todayItems.Add(new TodayItem
                {
                    StartTime = lesson.TimeStart.Substring(0, 5),
                    EndTime = lesson.TimeEnd.Substring(0, 5),
                    Title = lesson.SubjectName,
                    Description = $"{lesson.TeacherName}" 
                                  + (string.IsNullOrWhiteSpace(lesson.Location)
                                  ? string.Empty
                                  : $" ({lesson.Location})")
                });
            }

            // get calendar entries

            // get lunch




            todayItems = todayItems.OrderBy(t => t.StartTime).ToList();


            SchoolStartTime = timetable?.OrderBy(l => l.TimeStart).FirstOrDefault()?.TimeStart.Substring(0, 5) ?? "";
            SchoolEndTime = timetable?.OrderByDescending(l => l.TimeEnd).FirstOrDefault()?.TimeEnd.Substring(0, 5) ?? "";
            IsSportsbagDay = timetable?.Any(l => l.SubjectCode == "IDH") ?? false;
            

            MainThread.BeginInvokeOnMainThread(() =>
            {
                items.Clear();
                foreach (var item in todayItems)
                {
                    items.Add(item);
                }
            });

        }
    }


}
