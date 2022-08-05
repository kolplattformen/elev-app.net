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

        public LunchViewModel(SkolplattformenService skolplattformenService)
        {
            _skolplattformenService = skolplattformenService;
            items = new ObservableCollection<Meal>();

            Task.Run(LoadData);
        }

        private async Task LoadData()
        {
           // if (this.IsRefreshing) return;

            var today = DateTime.Now;
            var isoYear = ISOWeek.GetYear(today);
            var week = ISOWeek.GetWeekOfYear(today);

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
        private async Task Refresh()
        {
            await LoadData();
            IsRefreshing = false;
        }
    }
}
