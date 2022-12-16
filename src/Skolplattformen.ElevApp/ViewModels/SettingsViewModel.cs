using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Skolplattformen.ElevApp.Data;
using Skolplattformen.ElevApp.Pages;

namespace Skolplattformen.ElevApp.ViewModels
{
    public partial class SettingsViewModel: ObservableObject
    {
        private readonly SkolplattformenService _skolplattformenService;
        [ObservableProperty] private bool isLoading;
        [ObservableProperty] private string skolmatenSeSchoolName;
        [ObservableProperty] private bool useSkolmatenSe;
        [ObservableProperty] private bool showCalendarInTodayView;
        [ObservableProperty] private bool showPlannedAbsenceInTodayView;
        [ObservableProperty] private bool showKalendariumInTodayView;

        [ObservableProperty] private string schoolName;
        [ObservableProperty] private string studentName;

        public SettingsViewModel(SkolplattformenService skolplattformenService)
        {
            _skolplattformenService = skolplattformenService;
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
            Settings.UseSkolmatenSe = useSkolmatenSe;
            Settings.SkolmatenSeSchoolName = skolmatenSeSchoolName;
            Settings.ShowCalendarInTodayView = showCalendarInTodayView;
            Settings.ShowPlannedAbsenceInTodayView = showPlannedAbsenceInTodayView;
            Settings.ShowKalendariumInTodayView = showKalendariumInTodayView;
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

            UseSkolmatenSe = Settings.UseSkolmatenSe;
            SkolmatenSeSchoolName = Settings.SkolmatenSeSchoolName;
            ShowCalendarInTodayView = Settings.ShowCalendarInTodayView;
            ShowPlannedAbsenceInTodayView = Settings.ShowPlannedAbsenceInTodayView;
            ShowKalendariumInTodayView = Settings.ShowKalendariumInTodayView;
            

            var user = await _skolplattformenService.GetUserAsync();
            StudentName = user?.Name ?? "";

            var schoolDetails = await _skolplattformenService.GetSchoolDetailsAsync(user.PrimarySchoolGuid);
            SchoolName = schoolDetails.Name;

            IsLoading = false;
        }

        

        public Task OnActivated()
        {
            return  LoadData();
        }
    }

    
}
