using System.Collections.ObjectModel;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Skolplattformen.ElevApp.Data;
using SkolplattformenElevApi.Models;

namespace Skolplattformen.ElevApp.ViewModels
{
    public partial class LunchViewModel: ObservableObject
    {
        private readonly SkolplattformenService _skolplattformenService;
        [ObservableProperty] private ObservableCollection<Meal> items;
        [ObservableProperty] private string title;
        [ObservableProperty] private bool isRefreshing;
        [ObservableProperty] private DateTime day;

        public LunchViewModel(SkolplattformenService skolplattformenService)
        {
            _skolplattformenService = skolplattformenService;
            items = new ObservableCollection<Meal>();

            Day = DateTime.Now; // Skip weekends
            if (Day.DayOfWeek == DayOfWeek.Saturday)
            {
                Day = Day.AddDays(1);
            }
            if (Day.DayOfWeek == DayOfWeek.Sunday)
            {
                Day = Day.AddDays(1);
            }

            Task.Run(LoadData);
        }

        private async Task LoadData()
        {
           // if (this.IsRefreshing) return;

            
            var isoYear = ISOWeek.GetYear(Day);
            var week = ISOWeek.GetWeekOfYear(Day);

            var meals = await _skolplattformenService.GetMealsAsync(isoYear, week);
            foreach (var meal in meals)
            {
                meal.Title = meal.Title.Split(" ").FirstOrDefault() ?? meal.Title;
            }

            Title = $"Lunch vecka {week}";

            MainThread.BeginInvokeOnMainThread(() =>
            {
                items.Clear();
                foreach (var meal in meals.OrderBy(m => m.Date))
                {
                    items.Add(meal);
                }
            });
        }

        [RelayCommand]
        void Previous()
        {
            Day = Day.AddDays(-7);
            Task.Run(LoadData);
        }

        [RelayCommand]
        void Next()
        {
            Day = Day.AddDays(7);
            Task.Run(LoadData);
        }


        [RelayCommand]
        private async Task Refresh()
        {
            await LoadData();
            IsRefreshing = false;
        }

        public Task OnActivated()
        {
            return LoadData();
        }
    }
}
