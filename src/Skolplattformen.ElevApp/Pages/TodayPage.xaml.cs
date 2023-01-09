using Skolplattformen.ElevApp.ViewModels;

namespace Skolplattformen.ElevApp.Pages;

public partial class TodayPage : ContentPage
{
	public TodayPage(TodayViewModel model)
	{
		InitializeComponent();
        this.BindingContext = model;
    }

    protected override void OnAppearing()
    {
        var model = this.BindingContext as TodayViewModel;
        Task.Run(model.OnActivated);
    }
}