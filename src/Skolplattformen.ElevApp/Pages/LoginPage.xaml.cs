using Skolplattformen.ElevApp.ViewModels;

namespace Skolplattformen.ElevApp.Pages;

public partial class LoginPage : ContentPage
{
	
	public LoginPage(LoginViewModel model)
	{
		InitializeComponent();
        BindingContext = model;
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

