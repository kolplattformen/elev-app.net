using Skolplattformen.ElevApp.Resources.Themes;


namespace Skolplattformen.ElevApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        SwitchTheme();
    
        Application.Current.RequestedThemeChanged += (s, a) =>
        {
            SwitchTheme();
        };

        MainPage = new AppShell();
        
    }

    private void SwitchTheme()
    {
        ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries != null)
        {
            mergedDictionaries.Clear();

            switch (Application.Current.RequestedTheme)
            {
                case AppTheme.Dark:
                    mergedDictionaries.Add(new DarkTheme());
                    break;
                //  case AppTheme.Light:
                default:
                    mergedDictionaries.Add(new LightTheme());
                    break;
            }
        }
    }
}
