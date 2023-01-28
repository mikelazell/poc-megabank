using Megabank.Api.Interfaces;
using Megabank.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Megabank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IBankService _bankService;

        public AccountsController(ICurrentUserService currentUserService, IBankService bankService)
        {
            _currentUserService = currentUserService;
            _bankService = bankService;
        }

        [Authorize]
        [HttpGet(Name = "List")]
        public async Task<IEnumerable<AccountSummaryModel>> Get()
        {
            return _bankService.GetUserAccounts(_currentUserService.Id)
                .Select(AccountSummaryModel.FromAccountModel);
        }

        [Authorize]
        [HttpGet(template: "{id}", Name = "Details")]
        public async Task<AccountSummaryModel> Get(Guid id)
        {
            var account = _bankService.GetUserAccount(_currentUserService.Id, id);
            return AccountSummaryModel.FromAccountModel(account);
        }

        [Authorize]
        [HttpPost(template: "add", Name = "AddAccount")]
        public async Task<AccountSummaryModel> AddAccount(AddAccountModel addAccountModel)
        {
            var newAccount = _bankService.AddAccount(addAccountModel, _currentUserService.Id);
            return AccountSummaryModel.FromAccountModel(newAccount);
        }

        [Authorize]
        [HttpGet(template: "{id}/transfers", Name = "Transfers")]
        public async Task<IEnumerable<TransferSummaryModel>> Transfer(Guid id)
        {
            var transfers = _bankService.AccountTransfers(_currentUserService.Id, id, 1, 50);
            return transfers;
        }

        [Authorize]
        [HttpPost(template: "{id}/transfers/add", Name = "AddTransfer")]
        public async Task<ActionResult> AddTransfer(AddTransferModel addTransferModel)
        {
            _bankService.TransferMoney(addTransferModel);
            return new NoContentResult();
        }
    }
}
