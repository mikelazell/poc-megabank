using System.Net.Http.Json;
using Megabank.App.Models;
using Microsoft.AspNetCore.Components;

namespace Megabank.App.Pages;

public partial class CreateAccount
{
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private HttpClient HttpClient { get; set; }

    private AddAccountModel _addAccountModel = new();

    private async void HandleValidSubmit()
    {
        await HttpClient.PostAsJsonAsync("api/accounts/add", _addAccountModel);
        {
            NavigationManager.NavigateTo("accounts");
        }
    }
}