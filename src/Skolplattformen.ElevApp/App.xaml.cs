

using Skolplattformen.ElevApp.Resources.Themes;
#if __IOS__
using ObjCRuntime;
using UIKit;
using Foundation;
#endif
#if __ANDROID__
using Android.Content.Res;
#endif

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

    protected override void OnResume()
         {
        SwitchTheme();
    }

    private void SwitchTheme()
    {
        ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries != null)
        {
            mergedDictionaries.Clear();

            switch (MyWorkingRequestedTheme)
            {
                case AppTheme.Dark:
                    mergedDictionaries.Add(new DarkTheme());
                    Application.Current.UserAppTheme = AppTheme.Dark;
                    break;
                //  case AppTheme.Light:
                default:
                    mergedDictionaries.Add(new LightTheme());
                    Application.Current.UserAppTheme = AppTheme.Light;
                  
                    break;
            }

        }
    }

    // Below are fixes to make MAUI switch theme
    // https://github.com/dotnet/maui/issues/10729

#if __IOS__
private AppTheme MyWorkingRequestedTheme
		{
			get
			{
				if ((OperatingSystem.IsIOS() && !OperatingSystem.IsIOSVersionAtLeast(13, 0)) || (OperatingSystem.IsTvOS() && !OperatingSystem.IsTvOSVersionAtLeast(13, 0)))
					return AppTheme.Unspecified;

				var traits = 
					MainThread.InvokeOnMainThreadAsync(() => WindowStateManager.Default.GetCurrentUIViewController()?.TraitCollection)
                        .GetAwaiter().GetResult() ??
					UITraitCollection.CurrentTraitCollection;

				var uiStyle = traits.UserInterfaceStyle;

				return uiStyle switch
				{
					UIUserInterfaceStyle.Light => AppTheme.Light,
					UIUserInterfaceStyle.Dark => AppTheme.Dark,
					_ => AppTheme.Unspecified
				};
			}
		}
#elif __ANDROID__
    public AppTheme MyWorkingRequestedTheme => (Android.App.Application.Context.Resources.Configuration.UiMode & UiMode.NightMask) switch
    {
        UiMode.NightYes => AppTheme.Dark,
        UiMode.NightNo => AppTheme.Light,
        _ => AppTheme.Unspecified
    };
#else
    public AppTheme MyWorkingRequestedTheme => Application.Current.RequestedTheme;
#endif
}
