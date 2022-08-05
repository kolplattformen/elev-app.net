using Skolplattformen.ElevApp.ViewModels;

namespace Skolplattformen.ElevApp.Pages;

public partial class LunchPage : ContentPage
{
	public LunchPage(LunchViewModel model)
	{
		InitializeComponent();
        this.BindingContext = model;
    }
}