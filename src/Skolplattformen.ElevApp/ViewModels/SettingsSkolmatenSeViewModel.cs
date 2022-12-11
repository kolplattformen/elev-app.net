using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Skolplattformen.ElevApp.Data;

namespace Skolplattformen.ElevApp.ViewModels
{
    public partial class SettingsSkolmatenSeViewModel : ObservableObject
    {
        [ObservableProperty] private bool isLoading;
        [ObservableProperty] private string skolmatenSeSchoolName;
        [ObservableProperty] private string skolmatenSeSchooFullName;
        [ObservableProperty] private bool isSchoolNameValid;
        [ObservableProperty] private bool useSkolmatenSe;

        [RelayCommand]
        public void Save()
        {
            Settings.UseSkolmatenSe = useSkolmatenSe;
            Settings.SkolmatenSeSchoolName = skolmatenSeSchoolName;
        }

        [RelayCommand]
        public async Task Validate()
        {
            if (!string.IsNullOrEmpty(SkolmatenSeSchoolName))
            {
                var validationResult = await SkolmatenSeService.ValidateSchoolAsync(SkolmatenSeSchoolName);
                if (validationResult.valid)
                {
                    IsSchoolNameValid = true;
                    SkolmatenSeSchooFullName = validationResult.schoolName;
                }
            }
        }

        [RelayCommand]
        public async Task SaveAndPop()
        {
            
            Settings.SkolmatenSeSchoolName = skolmatenSeSchoolName;
            await Shell.Current.Navigation.PopAsync();
        }

        public Task LoadData()
        {
            if (IsLoading) return Task.CompletedTask;
            IsLoading = true;

            UseSkolmatenSe = Settings.UseSkolmatenSe;
            SkolmatenSeSchoolName = Settings.SkolmatenSeSchoolName;
            IsLoading = false;
            return Task.CompletedTask;
        }

        public Task OnActivated()
        {
            return LoadData();
        }
    }
        
    
}
