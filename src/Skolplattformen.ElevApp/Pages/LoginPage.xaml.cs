using Skolplattformen.ElevApp.ViewModels;

namespace Skolplattformen.ElevApp.Pages;

public partial class LoginPage : ContentPage
{
	
	public LoginPage(LoginViewModel model)
	{
		InitializeComponent();
        BindingContext = model;
    }
}

