using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Skolplattformen.ElevApp.Data;
using Skolplattformen.ElevApp.Models;
using Skolplattformen.ElevApp.Pages;

namespace Skolplattformen.ElevApp.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty] private bool isLoading = false;

        [ObservableProperty] private string email;
        [ObservableProperty] private string username;
        [ObservableProperty] private string password;
        [ObservableProperty] private ObservableCollection<string> platformList;
        [ObservableProperty] private int _platformSelectedIndex;

        private readonly SkolplattformenService _skolplattformenService;

        public LoginViewModel(SkolplattformenService skolplattformenService)
        {
            _skolplattformenService = skolplattformenService;

            PlatformList = new ObservableCollection<string>
            {
                "Skolplattformen Stockholm",
                "Demo"
            };
            PlatformSelectedIndex = 0;

            Task.Run(LoadData);
        }

        Task LoadData()
        {
            if(IsLoading) return Task.CompletedTask;
            IsLoading = true;
            var loginDetails = Storage.Get<LoginDetails>("login_details");
            if (loginDetails.RememberMe)
            {
                Email = loginDetails.Email;
                Username = loginDetails.Username;
                Password = loginDetails.Password;
            }

            PlatformSelectedIndex = loginDetails?.Platform ?? 0;

            IsLoading = false;
            return Task.CompletedTask;
        }


        [RelayCommand]
        async Task Login()
        {
            if(IsLoading) return; 
            IsLoading = true;

            ApiKind kind = PlatformSelectedIndex switch
            {
                0 => ApiKind.Skolplattformen,
                1 => ApiKind.FakeData,
                _ => ApiKind.FakeData
            };
            if (PlatformSelectedIndex != -1)
            {
                _skolplattformenService.SelectApi(kind);
            }

            var loginDetails = new LoginDetails
            {
                Username = username,
                Email = email,
                Password = password,
                RememberMe = true,
                Platform = PlatformSelectedIndex
            };
            Storage.Store("login_details", loginDetails);

            await _skolplattformenService.LogInAsync(email, username, password);

            IsLoading = false;
            await Shell.Current.GoToAsync($"//{nameof(TodayPage)}");
        }

        
    }
    
}
