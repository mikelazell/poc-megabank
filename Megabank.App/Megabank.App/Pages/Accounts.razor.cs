using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Megabank.App.Models;
using Microsoft.AspNetCore.Components;

namespace Megabank.App.Pages
{
    public partial class Accounts
    {
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] HttpClient HttpClient { get; set; }

        private IEnumerable<AccountSummaryModel> _accounts;

        protected override async Task OnInitializedAsync()
        {
            HttpResponseMessage response = await HttpClient.GetAsync("api/accounts");
            {
                string content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                _accounts = JsonSerializer.Deserialize<List<AccountSummaryModel>>(content, options);
            }

            await base.OnInitializedAsync();
        }

        void ViewAccount(Guid id)
        {
            NavigationManager.NavigateTo($"accounts/{id}");
        }

        void CreateAccount()
        {
            NavigationManager.NavigateTo($"accounts/create");
        }

    }
}
