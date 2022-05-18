using CommunityToolkit.Maui;

using GarageDoorOpener.App.Interfaces;
using GarageDoorOpener.App.Services;
using GarageDoorOpener.Client;

namespace GarageDoorOpener.App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			}).UseMauiCommunityToolkit();

		builder.Services.AddGarageDoorOpenerClient();

		builder.Services.AddHttpClient();
		builder.Services.AddSingleton<IClientFactory, ClientFactory>();

		builder.Services
			.AddTransient<AppShell>()
			.AddTransient<GarageDoorViewModel>()
			.AddTransient<GarageDoorPage>()
			.AddTransient<ConfigurationPage>()
			.AddTransient<ConfigurationPageViewModel>()
			.AddTransient<IGarageDoorService, GarageDoorService>()
			.AddTransient<IPreferenceService, PreferenceService>()
			.AddTransient<ISecureStorageService, SecureStorageService>();

		return builder.Build();
	}
}
