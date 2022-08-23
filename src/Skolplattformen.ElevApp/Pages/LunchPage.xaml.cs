using Skolplattformen.ElevApp.ViewModels;

namespace Skolplattformen.ElevApp.Pages;

public partial class LunchPage : ContentPage
{
	public LunchPage(LunchViewModel model)
	{
		InitializeComponent();
        this.BindingContext = model;
        model.NotifyScrollChangeAction = NotifyScrollChanged;
    }

    protected override void OnAppearing()
    {
        var model = this.BindingContext as LunchViewModel;
        
        Task.Run(model.OnActivated);
        
        
    }

    private void NotifyScrollChanged()
    {
        var content = ScrollViewCtrl.Content;
        ScrollViewCtrl.Content = null;
        ScrollViewCtrl.Content = content;
    }

  
}