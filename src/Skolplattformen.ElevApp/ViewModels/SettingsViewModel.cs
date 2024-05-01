using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Skolplattformen.ElevApp.ApiInterface;
using Skolplattformen.ElevApp.Data;
using Skolplattformen.ElevApp.Pages;

namespace Skolplattformen.ElevApp.ViewModels
{
    public partial class SettingsViewModel: ObservableObject
    {
        private readonly SkolplattformenService _skolplattformenService;
        [ObservableProperty] private bool isLoading;

        [ObservableProperty] private bool showCalendarInTodayView;
        [ObservableProperty] private bool showPlannedAbsenceInTodayView;
        [ObservableProperty] private bool showKalendariumInTodayView;

        [ObservableProperty] private string schoolName;
        [ObservableProperty] private string studentName;
        
        [ObservableProperty] private bool todayTabSettingsVisible;
        [ObservableProperty] private bool todayTabSettingsMyCalendarVisible;
        [ObservableProperty] private bool todayTabSettingsAbsenceVisible;
        [ObservableProperty] private bool todayTabSettingsKalendariumVisible;

        public SettingsViewModel(SkolplattformenService skolplattformenService)
        {
            _skolplattformenService = skolplattformenService;
        }

        [RelayCommand]
        public void OpenPrivacyPolicy()
        {
            Browser.OpenAsync("https://skolplattformen.org/integritetspolicy-elevapp");
        }


        [RelayCommand]
        async Task Logout()
        {
            _skolplattformenService.LogOut();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        [RelayCommand]
        public void Save()
        {
            Settings.ShowCalendarInTodayView = showCalendarInTodayView;
            Settings.ShowPlannedAbsenceInTodayView = showPlannedAbsenceInTodayView;
            Settings.ShowKalendariumInTodayView = showKalendariumInTodayView;

            Toast.Make("Inställningarna sparades").Show();

        }

        [RelayCommand]
        public async Task PopupSkolmaten()
        {
            await Shell.Current.Navigation.PushAsync(new SettingsSkolmatenSePage(new SettingsSkolmatenSeViewModel()));
       
        }

        public  async Task LoadData()
        {
            if(IsLoading) return ;
            IsLoading = true;

            ShowCalendarInTodayView = Settings.ShowCalendarInTodayView;
            ShowPlannedAbsenceInTodayView = Settings.ShowPlannedAbsenceInTodayView;
            ShowKalendariumInTodayView = Settings.ShowKalendariumInTodayView;

            SetVisibilityDependingOnApiFetures();
            try
            {
                var user = await _skolplattformenService.GetUserAsync();
                StudentName = user?.Name ?? "";

                if (user != null)
                {
                    var schoolDetails = await _skolplattformenService.GetSchoolDetailsAsync(user.PrimarySchoolGuid);
                    SchoolName = schoolDetails.Name;
                }

            }
            catch {/* do nothing */}
            MainThread.BeginInvokeOnMainThread(() =>
            {
                NotifyScrollChangeAction();
            });
            IsLoading = false;
        }

        private void SetVisibilityDependingOnApiFetures()
        {
            TodayTabSettingsMyCalendarVisible = _skolplattformenService.ApiFeatures.HasFlag(ApiFeatures.Calendar);
            TodayTabSettingsAbsenceVisible = _skolplattformenService.ApiFeatures.HasFlag(ApiFeatures.PlannedAbsence);
            TodayTabSettingsKalendariumVisible = _skolplattformenService.ApiFeatures.HasFlag(ApiFeatures.Kalendarium);

            TodayTabSettingsVisible = TodayTabSettingsMyCalendarVisible ||
                                      TodayTabSettingsAbsenceVisible ||
                                      TodayTabSettingsKalendariumVisible;

        }

        public Action NotifyScrollChangeAction;

        public Task OnActivated()
        {
            return  LoadData();
        }
    }

    
}
