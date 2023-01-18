using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Handlers;
using Skolplattformen.ElevApp.Data;
using Skolplattformen.ElevApp.Models;
using Skolplattformen.ElevApp.ApiInterface.Models;


namespace Skolplattformen.ElevApp.ViewModels
{
    public partial class TodayViewModel: ObservableObject
    {
        private readonly SkolplattformenService _skolplattformenService;
        [ObservableProperty] private bool isLoading;

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

        private string MonthSe(int month) => month switch
        {
            1 => "januari",
            2 => "februari",
            3 => "mars",
            4 => "april",
            5 => "maj",
            6 => "juni",
            7 => "juli",
            8 => "augusti",
            9 => "september",
            10 => "oktober",
            11 => "november",
            12 => "december",
            _ => "okänt"
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
        void Today()
        {
            CurrentDate = DateTime.Now;
            Task.Run(LoadData);
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
            if (IsLoading) return;
            IsLoading = true;

            Title = (currentDate.Date == DateTime.Now.Date)
                ? $"Idag {WeekdaySe(currentDate.DayOfWeek).ToLower()} {currentDate.Day} {MonthSe(currentDate.Month)}"
                : $"{WeekdaySe(currentDate.DayOfWeek)} {currentDate.Day} {MonthSe(currentDate.Month)}";
            
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
                    Mark = TodayItemMark.Lesson,
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
                            Mark = TodayItemMark.Calendar
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
                            Mark = TodayItemMark.Calendar
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
                            Mark = TodayItemMark.Absence
                        });
                    }
                    else
                    {
                        todayItems.Add(new TodayItem
                        {
                            StartTime = item.DateTimeFrom.ToString("HH:mm"),
                            EndTime = item.DateTimeTo.ToString("HH:mm"),
                            Title = item.ReasonDescription,
                            Description = item.Reporter,
                            Mark = TodayItemMark.Absence
                        });
                    }
                }
            }

            if (Settings.ShowKalendariumInTodayView)
            {
                var kalendarium = await _skolplattformenService.GetKalendariumAsync(currentDate);
                foreach (var item in kalendarium)
                {
                    if (item.IsAllDayEvent)
                    {
                        allDayItems.Add(new TodayItem
                        {
                            Title = item.Title,
                            Description = item.Description,
                            Mark = TodayItemMark.Kalendarium
                        });
                    }
                    else
                    {
                        todayItems.Add(new TodayItem
                        {
                            StartTime = item.StartDate.ToString("HH:mm"),
                            EndTime = item.EndDate.ToString("HH:mm"),
                            Title = item.Title,
                            Description = item.Description,
                            Mark = TodayItemMark.Kalendarium
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

                NotifyScrollChangeAction();
            });
            IsLoading = false;
        }

        public Action NotifyScrollChangeAction;

    }

    public class TodayPageDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PrimaryColorTemplate { get; set; }
        public DataTemplate AbsenceTemplate { get; set; }

        public DataTemplate KalendariumTemplate { get; set; }
        public DataTemplate CalendarTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {

            return ((TodayItem)item).Mark switch
            {
                TodayItemMark.Lesson => PrimaryColorTemplate,
                TodayItemMark.Absence => AbsenceTemplate,
                TodayItemMark.Kalendarium => KalendariumTemplate,
                TodayItemMark.Calendar => CalendarTemplate,
                _ => PrimaryColorTemplate
            };

        }
    }
}
