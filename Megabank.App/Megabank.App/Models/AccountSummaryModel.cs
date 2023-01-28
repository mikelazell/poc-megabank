namespace Megabank.App.Models
{
    public class AccountSummaryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }   
        public decimal Balance { get; set; }
        public int AccountNumber { get; set; }
        public int SortCode { get; set; }
    }
}
