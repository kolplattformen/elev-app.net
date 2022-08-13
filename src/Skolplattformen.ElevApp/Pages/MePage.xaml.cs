using Skolplattformen.ElevApp.ViewModels;

namespace Skolplattformen.ElevApp.Pages;

public partial class MePage : ContentPage
{
	public MePage(MeViewModel model)
	{
		InitializeComponent();
        this.BindingContext = model;
    }

    protected override void OnAppearing()
    {
        var model = this.BindingContext as MeViewModel;
        Task.Run(model.OnActivated);
    }
}