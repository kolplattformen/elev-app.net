using Skolplattformen.ElevApp.ViewModels;

namespace Skolplattformen.ElevApp.Pages;

public partial class SettingsSkolmatenSePage : ContentPage
{
	public SettingsSkolmatenSePage(SettingsSkolmatenSeViewModel model)
	{
		InitializeComponent();
        this.BindingContext = model;
        model.NotifyScrollChangeAction = NotifyScrollChanged;
    }

    protected override void OnAppearing()
    {
        var model = this.BindingContext as SettingsSkolmatenSeViewModel;
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

    private void Entry_Focused(object sender, FocusEventArgs e)
    {
        if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
        {
            layout.TranslateTo(0, -200, 50);
        }
    }

    private void Entry_Unfocused(object sender, FocusEventArgs e)
    {
        if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
        {
            layout.TranslateTo(0, 0, 50);
        }
    }
}
