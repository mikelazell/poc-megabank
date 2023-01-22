using Megabank.App.Auth;

namespace Megabank.App;

public partial class MainPage : ContentPage
{
    private readonly AuthClient _authClient;
    private bool _authenticated = false;
    public MainPage(AuthClient authClient)
    {
        _authClient = authClient;
        InitializeComponent();

#if WINDOWS
        _authClient.Browser = new WebViewBrowserAuthenticator(WebViewInstance);
#endif
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var loginResult = await _authClient.LoginAsync();

        _authenticated = !loginResult.IsError;

        LoginView.IsVisible = !_authenticated;
        blazorWebView.IsVisible = _authenticated;

        if (_authenticated)
        {
            TokenHolder.AccessToken = loginResult.AccessToken;
        }
        else
        {
            await DisplayAlert("Error", loginResult.ErrorDescription, "OK");
        }
    }
}
