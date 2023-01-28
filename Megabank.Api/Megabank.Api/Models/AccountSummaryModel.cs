namespace Megabank.Api.Models
{
    public class AccountSummaryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }   
        public decimal Balance { get; set; }
        public int AccountNumber { get; init; }
        public int SortCode { get; init; }

        public static AccountSummaryModel FromAccountModel(AccountModel accountModel)
        {
            return new AccountSummaryModel
            {
                Balance = accountModel.Balance,
                Id = accountModel.Id,
                Name = accountModel.Name,
                SortCode = accountModel.SortCode,
                AccountNumber = accountModel.AccountNumber
            };
        }
    }
}
