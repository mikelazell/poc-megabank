namespace Megabank.Api.Models
{
    public class AccountModel
    {
        public AccountModel()
        {
            Id = Guid.NewGuid();
            Transfers = new List<TransferModel>();
        }
        public Guid Id { get; init; }
        public string UserId { get; init; }
        public string Name { get; init; }   
        public decimal Balance { get; private set; }
        public int AccountNumber { get; init; }
        public int SortCode { get; init; }
        public IList<TransferModel>? Transfers { get; set; }

        public static AccountModel Create(int sortCode, int accountNumber, string name, string userId)
        {
            return new AccountModel
            {
                AccountNumber = accountNumber,
                SortCode = sortCode,
                Name = name,
                UserId = userId
            };
        }

        public void TransferMoneyOut(string reference, decimal value, AccountModel destinationAccount)
        {
            TransferModel transfer = new TransferModel
            {
                Date = DateTime.UtcNow,
                SourceAccountId = Id,
                DestinationAccountId = destinationAccount.Id,
                Value = value,
                Reference = reference
            };
            Transfers.Add(transfer);
            Balance -= value;
            destinationAccount.TransferMoneyIn(transfer);
        }

        public void TransferMoneyIn(TransferModel transferModel)
        {
            Transfers.Add(transferModel);
            Balance += transferModel.Value;
        }
    }
}
