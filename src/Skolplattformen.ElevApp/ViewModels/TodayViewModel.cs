using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

        private string WeekdaySe(DayOfWeek dow) => dow switch
        {
            DayOfWeek.Monday => "Måndag",
            DayOfWeek.Tuesday => "Tisdag",
            DayOfWeek.Wednesday => "Onsdag",
            DayOfWeek.Thursday => "Torsdag",
            DayOfWeek.Friday => "Fredag",
            DayOfWeek.Saturday => "Lördag",
            _=> "Söndag"
        };

        public TodayViewModel(SkolplattformenService skolplattformenService)
        {
            _skolplattformenService = skolplattformenService;

            items = new ObservableCollection<TodayItem>();
            if (CurrentDate.DayOfWeek == DayOfWeek.Saturday)
            {
                CurrentDate = CurrentDate.AddDays(1);
            }
            if (CurrentDate.DayOfWeek == DayOfWeek.Sunday)
            {
                CurrentDate = CurrentDate.AddDays(1);
            }

        }

        [RelayCommand]
        void Previous()
        {
            CurrentDate = currentDate.AddDays(-1);
            if (CurrentDate.DayOfWeek == DayOfWeek.Sunday)
            {
                CurrentDate = CurrentDate.AddDays(-1);
            }
            if (CurrentDate.DayOfWeek == DayOfWeek.Saturday)
            {
                CurrentDate = CurrentDate.AddDays(-1);
            }
            Task.Run(LoadData);
        }

        [RelayCommand]
        void Next()
        {
            CurrentDate = currentDate.AddDays(1);
            if (CurrentDate.DayOfWeek == DayOfWeek.Saturday)
            {
                CurrentDate = CurrentDate.AddDays(1);
            }
            if (CurrentDate.DayOfWeek == DayOfWeek.Sunday)
            {
                CurrentDate = CurrentDate.AddDays(1);
            }
            Task.Run(LoadData);
        }

        public Task OnActivated()
        {
            return LoadData();
        }

        public async Task LoadData()
        {
            Title = (currentDate.Date == DateTime.Now.Date)
                ? $"Idag {WeekdaySe(currentDate.DayOfWeek).ToLower()} {currentDate.ToString("dd/MM")}"
                : $"{WeekdaySe(currentDate.DayOfWeek)} {currentDate.ToString("dd/MM")}";
            
            var todayItems = new List<TodayItem>();
            var allDayItems = new List<TodayItem>();

            // get lunch
            var lunch = await _skolplattformenService.GetMealAsync(CurrentDate);
            
            // get lessons

            var timetable = await _skolplattformenService.GetTimetableAsync(currentDate);
            foreach (var lesson in timetable)
            {

                var item = new TodayItem
                {
                    StartTime = lesson.TimeStart.Substring(0, 5),
                    EndTime = lesson.TimeEnd.Substring(0, 5),
                    Title = lesson.SubjectName,
                    Description = $"{lesson.TeacherName}"
                                  + (string.IsNullOrWhiteSpace(lesson.Location)
                                      ? string.Empty
                                      : $" ({lesson.Location})")
                };

                if (lesson.SubjectCode.ToUpper() == "LUNCH")
                {
                    item.Description = lunch?.Description;
                }

                todayItems.Add(item);
                
            }

            // get calendar entries
            if (Settings.ShowCalendarInTodayView)
            {
                var calendar = await _skolplattformenService.GetCalendarAsync(currentDate);
                foreach (var item in calendar)
                {
                    if (item.IsAllDay)
                    {
                        allDayItems.Add(new TodayItem
                        {
                            Title = item.Title,
                            Mark = TodayItemMark.AllDay
                        });
                    }
                    else
                    {
                        todayItems.Add(new TodayItem
                        {
                            Title = item.Title,
                            Description = item.Location,
                            StartTime = item.Start.ToString("HH:mm"),
                            EndTime = item.End.ToString("HH:mm"),
                            Mark = TodayItemMark.Warning
                        });
                    }
                }
            }

            // get planned absence
            if (Settings.ShowPlannedAbsenceInTodayView)
            {
                var absence = await _skolplattformenService.GetPlannedAbsenceAsync(currentDate);
                foreach (var item in absence)
                {
                    if (item.IsFullDayAbsence)
                    {
                        allDayItems.Add(new TodayItem
                        {
                            Title = item.ReasonDescription,
                            Description = item.Reporter,
                            Mark = TodayItemMark.AllDay
                        });
                    }
                    else
                    {
                        todayItems.Add(new TodayItem
                        {
                            StartTime = item.DateTimeFrom.ToString("´HH:mm"),
                            EndTime = item.DateTimeTo.ToString("HH:mm"),
                            Title = item.ReasonDescription,
                            Description = item.Reporter

                        });
                    }
                }
            }
               


            todayItems = todayItems.OrderBy(t => t.StartTime).ToList();


            SchoolStartTime = timetable?.OrderBy(l => l.TimeStart).FirstOrDefault()?.TimeStart.Substring(0, 5) ?? "";
            SchoolEndTime = timetable?.OrderByDescending(l => l.TimeEnd).FirstOrDefault()?.TimeEnd.Substring(0, 5) ?? "";
            IsSportsbagDay = timetable?.Any(l => l.SubjectCode == "IDH") ?? false;
            

            MainThread.BeginInvokeOnMainThread(() =>
            {
                items.Clear();

                foreach (var item in allDayItems)
                {
                    items.Add(item);
                }
                foreach (var item in todayItems)
                {
                    items.Add(item);
                }
            });

        }

    
    }


}
