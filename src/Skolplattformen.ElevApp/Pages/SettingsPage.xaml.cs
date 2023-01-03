using Skolplattformen.ElevApp.ViewModels;

namespace Skolplattformen.ElevApp.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel model)
	{
		InitializeComponent();
        this.BindingContext = model;
        model.NotifyScrollChangeAction = NotifyScrollChanged;
    }

    protected override void OnAppearing()
    {
        var model = this.BindingContext as SettingsViewModel;
        Task.Run(model.OnActivated);
    }

    private void NotifyScrollChanged()
    {
        if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
        {
            var content = ScrollViewCtrl.Content;
            ScrollViewCtrl.Content = null;
            ScrollViewCtrl.Content = content;
        }
    }
}