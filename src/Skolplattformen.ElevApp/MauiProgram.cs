using CommunityToolkit.Maui;
using Skolplattformen.ElevApp.Data;
using Skolplattformen.ElevApp.Pages;
using Skolplattformen.ElevApp.ViewModels;

namespace Skolplattformen.ElevApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
                fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                fonts.AddFont("Poppins-SemiBold.ttf", "PoppinsSemibold");
            });

        builder.Services.AddSingleton<SkolplattformenService>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<TeachersPage>();
        builder.Services.AddSingleton<TeachersViewModel>();
        builder.Services.AddSingleton<TodayPage>();
        builder.Services.AddSingleton<TodayViewModel>();

        builder.Services.AddSingleton<LunchPage>();
        builder.Services.AddSingleton<LunchViewModel>();

        builder.Services.AddSingleton<SettingsPage>();
        builder.Services.AddSingleton<SettingsViewModel>();

        return builder.Build();
	}
}
