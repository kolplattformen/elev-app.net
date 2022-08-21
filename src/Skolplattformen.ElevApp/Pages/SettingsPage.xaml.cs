using Skolplattformen.ElevApp.ViewModels;

namespace Skolplattformen.ElevApp.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel model)
	{
		InitializeComponent();
        this.BindingContext = model;
    }

    protected override void OnAppearing()
    {
        var model = this.BindingContext as SettingsViewModel;
        Task.Run(model.OnActivated);
    }
}