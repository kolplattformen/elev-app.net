using Skolplattformen.ElevApp.ViewModels;

namespace Skolplattformen.ElevApp.Pages;

public partial class LunchPage : ContentPage
{
	public LunchPage(LunchViewModel model)
	{
		InitializeComponent();
        this.BindingContext = model;
    }

    protected override void OnAppearing()
    {
        var model = this.BindingContext as LunchViewModel;
        Task.Run(model.OnActivated);
    }
}