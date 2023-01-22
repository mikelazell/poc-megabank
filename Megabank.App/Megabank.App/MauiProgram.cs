using Megabank.App.Auth;
using Microsoft.Extensions.Logging;
using Megabank.App.Data;
using Megabank.App.Configuration.AppSettings;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Megabank.App;

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
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        var appConfiguration = GetAppConfiguration(builder);

        builder.Services.AddSingleton(new AuthClient(new()
        {
            Domain = appConfiguration.Auth.Domain,
            ClientId = appConfiguration.Auth.ClientId,
            Scope = "openid profile",
            Audience = appConfiguration.Auth.Audience,
            RedirectUri = appConfiguration.Auth.RedirectUri
        }));

        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddSingleton<TokenHandler>();

        builder.Services.AddSingleton<WeatherForecastService>();

		return builder.Build();
	}

    private static string GetPlatformCode()
    {
        if (DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst || DeviceInfo.Current.Platform == DevicePlatform.macOS)
        {
            return "mac";
        }
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            return "windows";
        }

        return DeviceInfo.Current.Platform.ToString().ToLower();
    }

    private static AppSettings GetAppConfiguration(MauiAppBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var appSettingsStream = assembly.GetManifestResourceStream("Megabank.App.appsettings.json");
        using var appSettingsPlatformStream = assembly.GetManifestResourceStream($"Megabank.App.appsettings.{GetPlatformCode()}.json");

        var config = new ConfigurationBuilder()
            .AddJsonStream(appSettingsStream)
            .AddJsonStream(appSettingsPlatformStream)
            .Build();

        builder.Configuration.AddConfiguration(config);

        return config.Get<AppSettings>();
    }
}
