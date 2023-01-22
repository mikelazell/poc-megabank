using System.Reflection;
using MauiAuth.Auth0;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Http;
using MauiAuth.Configuration.AppSettings;

namespace MauiAuth;

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
            });

#if DEBUG
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
        builder.Services.AddHttpClient("DemoAPI",
            client => client.BaseAddress = new Uri(appConfiguration.ApiUrl)
        ).AddHttpMessageHandler<TokenHandler>();
        builder.Services.AddTransient(
            sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("DemoAPI")
        );

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
        using var appSettingsStream = assembly.GetManifestResourceStream("MauiAuth.appsettings.json");
        using var appSettingsPlatformStream = assembly.GetManifestResourceStream($"MauiAuth.appsettings.{GetPlatformCode()}.json");

        var config = new ConfigurationBuilder()
            .AddJsonStream(appSettingsStream)
            .AddJsonStream(appSettingsPlatformStream)
            .Build();

        builder.Configuration.AddConfiguration(config);

        return config.Get<AppSettings>();
    }
}