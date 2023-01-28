using Megabank.Api.Models;

namespace Megabank.Api.Interfaces;

public interface IBankService
{
    AccountModel AddAccount(AddAccountModel newAccount, string userId);
    IList<AccountModel> GetUserAccounts(string userId);
    AccountModel GetUserAccount(string userId, Guid accountId);
    void TransferMoney(AddTransferModel addTransferModel);
    IList<TransferSummaryModel> AccountTransfers(string userId, Guid accountId, int page, int pageSize);
}