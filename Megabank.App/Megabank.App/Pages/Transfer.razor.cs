using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Megabank.App.Models;
using Microsoft.AspNetCore.Components;

namespace Megabank.App.Pages
{
    public partial class Transfer
    {
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] HttpClient HttpClient { get; set; }
        [Parameter] public Guid? AccountId { get; set; }

        private AddTransferModel _addTransferModel = new();

        private IEnumerable<AccountSummaryModel> _sourceAccounts;

        protected override async Task OnInitializedAsync()
        {
            _addTransferModel.SourceAccountId = AccountId;

            HttpResponseMessage response = await HttpClient.GetAsync("api/accounts");
            {
                string content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                _sourceAccounts = JsonSerializer.Deserialize<List<AccountSummaryModel>>(content, options);
            }

            await base.OnInitializedAsync();
        }

        private async void HandleValidSubmit()
        {
            var response = await HttpClient.PostAsJsonAsync($"api/accounts/{_addTransferModel.SourceAccountId}/transfers/add", _addTransferModel);
            {
                string content = await response.Content.ReadAsStringAsync();
                NavigationManager.NavigateTo($"accounts/{_addTransferModel.SourceAccountId}");
            }
        }
    }
}
