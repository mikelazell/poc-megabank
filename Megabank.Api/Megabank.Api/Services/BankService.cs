using Megabank.Api.Interfaces;
using Megabank.Api.Models;

namespace Megabank.Api.Services
{
    public class BankService : IBankService
    {
        private int _bankSortCode = 102030;
        private Random _random = new Random();
        private readonly IList<AccountModel> _accounts;

        public BankService()
        {
            _accounts = new List<AccountModel>();
        }

        public AccountModel AddAccount(AddAccountModel addAccountModel, string userId)
        {

            int accountNumber = _random.Next(10000000, 99999999);

            AccountModel newAccount = AccountModel.Create(_bankSortCode, accountNumber, addAccountModel.Name, userId);

            _accounts.Add(newAccount);

            return newAccount;
        }

        public IList<AccountModel> GetUserAccounts(string userId)
        {
            var accounts = _accounts.Where(a => a.UserId == userId).ToList();

            if (!accounts.Any())
            {
                var newAccount = AddAccount(new AddAccountModel
                {
                    Name = "Current Account"
                }, userId);

                accounts.Add(newAccount);
            }

            return accounts;
        }

        public AccountModel GetUserAccount(string userId, Guid accountId)
        {
            return _accounts.SingleOrDefault(a => a.UserId == userId && a.Id == accountId);
        }

        public IList<TransferSummaryModel> AccountTransfers(string userId, Guid accountId, int page, int pageSize)
        {
            var transfers = _accounts.Single(a => a.Id == accountId && a.UserId == userId)
                .Transfers
                .OrderByDescending(t => t.Date)
                .Skip((page - 1) * pageSize).Take(pageSize);

            return transfers.Select(t =>
            {
                var model = TransferSummaryModel.FromTransferModel(accountId, t);

                model.DestinationAccountName = _accounts.Single(a => a.Id == t.DestinationAccountId).Name;
                model.SourceAccountName = _accounts.Single(a => a.Id == t.SourceAccountId).Name;

                return model;
            }).ToList();
        }

        public void TransferMoney(AddTransferModel addTransferModel)
        {
            var sourceAccount = _accounts.Single(a => a.Id == addTransferModel.SourceAccountId);
            var destinationAccount = _accounts.Single(a => a.SortCode == addTransferModel.DestinationSortCode && a.AccountNumber == addTransferModel.DestinationAccountNumber);

            sourceAccount.TransferMoneyOut(addTransferModel.Reference, addTransferModel.Value, destinationAccount);
        }
    }
}
