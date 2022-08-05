using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Skolplattformen.ElevApp.Data;
using Skolplattformen.ElevApp.Pages;

namespace Skolplattformen.ElevApp.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty] private string email;
        [ObservableProperty] private string username;
        [ObservableProperty] private string password;
        [ObservableProperty] private ObservableCollection<string> plattformList;
        [ObservableProperty] private int plattformSelectedIndex;

        private readonly SkolplattformenService _skolplattformenService;

        public LoginViewModel(SkolplattformenService skolplattformenService)
        {
            _skolplattformenService = skolplattformenService;

            PlattformList = new ObservableCollection<string>
            {
                "Skolplattformen",
                "Demo"
            };
            PlattformSelectedIndex = 0;
        }

        [RelayCommand]
        async Task Login()
        {

            ApiKind kind = PlattformSelectedIndex switch
            {
                0 => ApiKind.Skolplattformen,
                1 => ApiKind.FakeData,
                _ => ApiKind.FakeData
            };
            if (PlattformSelectedIndex != -1)
            {
                _skolplattformenService.SelectApi(kind);
            }

            await _skolplattformenService.LogInAsync(email, username, password);

            await Shell.Current.GoToAsync($"//{nameof(TodayPage)}");
        }

        
    }
    
}
