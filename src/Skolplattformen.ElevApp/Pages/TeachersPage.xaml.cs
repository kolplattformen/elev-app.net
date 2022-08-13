using Skolplattformen.ElevApp.ViewModels;

namespace Skolplattformen.ElevApp.Pages;

public partial class TeachersPage : ContentPage
{
    public TeachersPage(TeachersViewModel model)
    {
        InitializeComponent();
        this.BindingContext = model;
    }

    protected override void OnAppearing()
    {
        var model = this.BindingContext as TeachersViewModel;
        Task.Run(model.OnActivated);
    }
}