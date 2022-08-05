using Skolplattformen.ElevApp.ViewModels;

namespace Skolplattformen.ElevApp.Pages;

public partial class LoginPage : ContentPage
{
	
	public LoginPage(LoginViewModel model)
	{
		InitializeComponent();
        BindingContext = model;
        picker.ItemsSource = new List<string>
        {
            "Skolplattformen",
            "Demo"
        };
        picker.SelectedIndex = 1;

    }

	//private void OnCounterClicked(object sender, EventArgs e)
	//{
	//	count++;

	//	if (count == 1)
	//		CounterBtn.Text = $"Clicked {count} time";
	//	else
	//		CounterBtn.Text = $"Clicked {count} times";

	//	SemanticScreenReader.Announce(CounterBtn.Text);

 //        Shell.Current.GoToAsync($"//{nameof(TodayPage)}").GetAwaiter().GetResult();


 //   }
 private void Picker_OnSelectedIndexChanged(object sender, EventArgs e)
 {
     
 }
}

