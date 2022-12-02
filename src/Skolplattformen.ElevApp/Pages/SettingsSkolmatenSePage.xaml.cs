using Skolplattformen.ElevApp.ViewModels;

namespace Skolplattformen.ElevApp.Pages;

public partial class SettingsSkolmatenSePage : ContentPage
{
	public SettingsSkolmatenSePage(SettingsSkolmatenSeViewModel model)
	{
		InitializeComponent();
        this.BindingContext = model;
    }

    protected override void OnAppearing()
    {
        var model = this.BindingContext as SettingsSkolmatenSeViewModel;
        Task.Run(model.OnActivated);
    }
}
