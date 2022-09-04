using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Skolplattformen.ElevApp.Data;
using Skolplattformen.ElevApp.Pages;
using SkolplattformenElevApi;
using SkolplattformenElevApi.Models;
using System.Collections.ObjectModel;

namespace Skolplattformen.ElevApp.ViewModels
{
    public partial class MeViewModel : ObservableObject
    {
        private readonly SkolplattformenService _skolplattformenService;

        // Student
        [ObservableProperty] private string studentName = string.Empty;
        
        // School
        [ObservableProperty] private string schoolName = string.Empty;
        [ObservableProperty] private string phone = string.Empty;

        [ObservableProperty] private string street = string.Empty;
        [ObservableProperty] private string postalCode = string.Empty;
        [ObservableProperty] private string locality = string.Empty;
        [ObservableProperty] private string visitingAddress = string.Empty;
        [ObservableProperty] private string schoolEmail = string.Empty;
        [ObservableProperty] private string principalName = string.Empty;
        [ObservableProperty] private string principalPhone = string.Empty;
        [ObservableProperty] private string principalEmail = string.Empty;
        
        //[ObservableProperty] private Dictionary<string, ApiReadSuccessIndicator> status = new Dictionary<string, ApiReadSuccessIndicator>();

        [ObservableProperty] private ObservableCollection<KeyValuePair<string, string>> status;

        [RelayCommand]
        async Task Logout()
        {
            _skolplattformenService.LogOut();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        public MeViewModel(SkolplattformenService skolplattformenService)
        {
            _skolplattformenService = skolplattformenService;
            status = new ObservableCollection<KeyValuePair<string, string>>();

            //    Task.Run(LoadData);
        }

        private async Task LoadData()
        {
            
            var user = await _skolplattformenService.GetUserAsync();
            StudentName = user?.Name ?? "";
            
            var schoolDetails = await _skolplattformenService.GetSchoolDetailsAsync(user.PrimarySchoolGuid);
            SchoolName = schoolDetails.Name;
            Phone = schoolDetails.Phone;
            PostalCode = schoolDetails.PostalCode;
            Street = schoolDetails.Street;
            Locality = schoolDetails.Locality;
            VisitingAddress = schoolDetails.VisitingAddress;
            SchoolEmail = schoolDetails.Email;
            PrincipalName = schoolDetails.PrincipalName;
            PrincipalPhone = schoolDetails.PrincipalPhone;
            PrincipalEmail = schoolDetails.PrincipalEmail;

            var stats = _skolplattformenService.GetStatusAll();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Status.Clear();
                foreach (var stat in stats)
                {
                    Status.Add(new KeyValuePair<string, string>(stat.Key, stat.Value.ToString()));
                }
            });

        }

        public Task OnActivated()
        {
            return LoadData();
        }
    }
}
