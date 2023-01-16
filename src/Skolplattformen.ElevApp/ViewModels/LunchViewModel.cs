using System.Collections.ObjectModel;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Skolplattformen.ElevApp.Data;
using Skolplattformen.ElevApp.Models;
using Skolplattformen.ElevApp.ApiInterface.Models;    

namespace Skolplattformen.ElevApp.ViewModels
{
    public partial class LunchViewModel: ObservableObject
    {
        private readonly SkolplattformenService _skolplattformenService;
        [ObservableProperty] private bool isLoading;

        [ObservableProperty] private ObservableCollection<LunchItem> items;
        [ObservableProperty] private string title;
        [ObservableProperty] private bool isRefreshing;
        [ObservableProperty] private DateTime day;
        [ObservableProperty] private bool usingSkolmatenSe;

        public LunchViewModel(SkolplattformenService skolplattformenService)
        {
            _skolplattformenService = skolplattformenService;
            items = new ObservableCollection<LunchItem>();

            Day = DateTime.Now; // Skip weekends
            if (Day.DayOfWeek == DayOfWeek.Saturday)
            {
                Day = Day.AddDays(1);
            }
            if (Day.DayOfWeek == DayOfWeek.Sunday)
            {
                Day = Day.AddDays(1);
            }

//Task.Run(async () => await LoadData());
        }

        private async Task LoadData()
        {
           // if (this.IsRefreshing) return;
           if (IsLoading) return;
           IsLoading = true; 

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
                    items.Add(new LunchItem
                    {
                        Date = meal.Date,
                        Title = meal.Title,
                        Description = meal.Description
                    });
                }

                UsingSkolmatenSe = Settings.UseSkolmatenSe;
                NotifyScrollChangeAction();
                
            });

            IsLoading = false;
        }

        public Action NotifyScrollChangeAction;
        

        [RelayCommand]
        void Previous()
        {
            Day = Day.AddDays(-7);
            Task.Run(LoadData);
        }

        [RelayCommand]
        void Today()
        {
            Day = DateTime.Now;
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

    public class LunchPageDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate StandardTemplate { get; set; }
        public DataTemplate ActiveTemplate { get; set; }

        
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((LunchItem)item).IsToday switch
            {
                true => ActiveTemplate,
                _ => StandardTemplate
            };

        }
    }
}
