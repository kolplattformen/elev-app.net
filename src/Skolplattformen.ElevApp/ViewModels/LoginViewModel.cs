using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Skolplattformen.ElevApp.ApiInterface;
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

        [ObservableProperty] private ObservableCollection<DexterApi.Installation> dexterInstallations;
        [ObservableProperty] private DexterApi.Installation selectedDexterInstallation;

        private readonly SkolplattformenService _skolplattformenService;

        public LoginViewModel(SkolplattformenService skolplattformenService)
        {
            _skolplattformenService = skolplattformenService;

            DexterInstallations = new ObservableCollection<DexterApi.Installation>(DexterApi.DexterApi.Installations);

            PlatformList = new ObservableCollection<string>
            {
                "Skolplattformen Stockholm",
                "IST Dexter",
                "Demo",
            };
            PlatformSelectedIndex = 0;

            Task.Run(LoadData);
        }

        Task LoadData()
        {
            if(IsLoading) return Task.CompletedTask;
            //     IsLoading = true;

            var loginDetails = Storage.Get<LoginDetails>("login_details")
                ?? new LoginDetails
                    {
                        RememberMe = false,
                        Platform = 0
                    };
            

            if (loginDetails.RememberMe)
            {
                Email = loginDetails.Email;
                Username = loginDetails.Username;
                Password = loginDetails.Password;
                SelectedDexterInstallation = DexterApi.DexterApi.Installations.Find(x => x.Id == loginDetails.DexterInstallation) 
                    ?? DexterApi.DexterApi.Installations.First();
            }
           
            PlatformSelectedIndex = loginDetails?.Platform ?? 0;

        //    IsLoading = false;
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
                1 => ApiKind.Dexter,
                _ => ApiKind.FakeData
            };
            if (PlatformSelectedIndex != -1)
            {
                _skolplattformenService.SelectApi(kind);
               
                if (App.Current.MainPage is AppShell shell)
                {
                    shell.IsTeachersTabVisible = _skolplattformenService.ApiFeatures.HasFlag(ApiFeatures.Teachers);
                }

            }

            var loginDetails = new LoginDetails
            {
                Username = username,
                Email = email,
                Password = password,
                RememberMe = true,
                Platform = PlatformSelectedIndex,
                DexterInstallation = SelectedDexterInstallation?.Id
            };
            Storage.Store("login_details", loginDetails);
            
            object loginCredentials = kind switch
            {
                ApiKind.Skolplattformen => new SkolplattformenApi.LoginCredentials() {Email = email, Username = username, Password = password },
                ApiKind.Dexter => new DexterApi.LoginCredentials() {Username = username, Password = password, InstallationId = SelectedDexterInstallation.Id },
                _ => new { username, password }
            };
            
            try
            {
                await _skolplattformenService.LogInAsync(loginCredentials);

                await Shell.Current.GoToAsync($"//{nameof(TodayPage)}");
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Något blev fel", "Kontrollera uppgifterna och försök igen", "Ok");
            }
            finally
            {
                IsLoading = false;
            }
        }

        
    }
    
}
