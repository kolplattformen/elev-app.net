using Skolplattformen.ElevApp.ViewModels;

namespace Skolplattformen.ElevApp.Pages;

public partial class TeachersPage : ContentPage
{
    public TeachersPage(TeachersViewModel model)
    {
        InitializeComponent();
        this.BindingContext = model;
    }

}