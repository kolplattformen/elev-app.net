using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Skolplattformen.ElevApp.Data;

namespace Skolplattformen.ElevApp.ViewModels
{
    public partial class SettingsSkolmatenSeViewModel : ObservableObject
    {
        [ObservableProperty] private bool isLoading;
        [ObservableProperty] private string skolmatenSeSchoolName;
        [ObservableProperty] private string skolmatenSeSchoolFullName;
        [ObservableProperty] private string skolmatenSeSchoolUrl;
        [ObservableProperty] private bool useSkolmatenSe;

        [RelayCommand]
        public void Save()
        {
            Settings.UseSkolmatenSe = useSkolmatenSe;
            Settings.SkolmatenSeSchoolName = skolmatenSeSchoolName;
        }

        
        public async Task<bool> Validate(string schoolName)
        {
            if (!string.IsNullOrEmpty(schoolName))
            {
                var validationResult = await SkolmatenSeService.ValidateSchoolAsync(schoolName);
                return validationResult.valid;
            }
            return false;
        }


        [RelayCommand]
        public async Task CancelAndPop()
        {
            SkolmatenSeSchoolName = Settings.SkolmatenSeSchoolName;
            
            await Shell.Current.Navigation.PopAsync();
        }

        [RelayCommand]
        public async Task SaveAndPop()
        {
            SkolmatenSeSchoolName = SchoolUrlToSchoolName(SkolmatenSeSchoolUrl);
            
            if (!UseSkolmatenSe ||  await Validate(SkolmatenSeSchoolName))
            {
                Settings.SkolmatenSeSchoolName = skolmatenSeSchoolName;
                Settings.UseSkolmatenSe = UseSkolmatenSe;
                await Shell.Current.Navigation.PopAsync();
                return;
            }

            await Toast.Make("Fel. Kan inte hämta skola på den länken.", ToastDuration.Long).Show();
        }
        
        public Task LoadData()
        {
            if (IsLoading) return Task.CompletedTask;
            IsLoading = true;

            UseSkolmatenSe = Settings.UseSkolmatenSe;
            SkolmatenSeSchoolName = Settings.SkolmatenSeSchoolName;
            IsLoading = false;

            SkolmatenSeSchoolUrl = SchoolNameToSchoolUrl(SkolmatenSeSchoolName);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                NotifyScrollChangeAction();
            });

            return Task.CompletedTask;
        }

        public Action NotifyScrollChangeAction;

        public Task OnActivated()
        {
            return LoadData();
        }

        private string SchoolUrlToSchoolName(string url)
        {
            url = url.Trim();
            while (url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }

            return url?.Split('/').LastOrDefault();
        }

        private string SchoolNameToSchoolUrl(string name) => !string.IsNullOrWhiteSpace(name) ? $"https://skolmaten.se/{name}/": "";
    }
        
    
}
