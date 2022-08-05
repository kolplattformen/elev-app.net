using Skolplattformen.ElevApp.ViewModels;

namespace Skolplattformen.ElevApp.Pages;

public partial class MePage : ContentPage
{
	public MePage(MeViewModel model)
	{
		InitializeComponent();
        this.BindingContext = model;
    }
}