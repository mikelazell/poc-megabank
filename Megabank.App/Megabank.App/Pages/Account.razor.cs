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
    public partial class Account
    {
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] HttpClient HttpClient { get; set; }
        [Parameter] public Guid AccountId { get; set; }

        private AccountSummaryModel _account; 
        private IEnumerable<TransferSummaryModel> _transfers;

        protected override async Task OnInitializedAsync()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            HttpResponseMessage response = await HttpClient.GetAsync($"api/accounts/{AccountId}");
            {
                string content = await response.Content.ReadAsStringAsync();
                _account = JsonSerializer.Deserialize<AccountSummaryModel>(content, options);
            }

            response = await HttpClient.GetAsync($"api/accounts/{AccountId}/transfers");
            {
                string content = await response.Content.ReadAsStringAsync();
                _transfers = JsonSerializer.Deserialize<IEnumerable<TransferSummaryModel>>(content, options);
            }
        }

        void TransferMoney()
        {
            NavigationManager.NavigateTo($"accounts/{AccountId}/transfer");
        }
    }
}
