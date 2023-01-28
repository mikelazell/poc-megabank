namespace Megabank.App.Models;

public class TransferSummaryModel
{
    public Guid Id { get; init; }
    public DateTime Date { get; set; }
    public string Reference { get; set; }
    public decimal Value { get; set; }
    public Guid SourceAccountId { get; set; }
    public string SourceAccountName { get; set; }
    public Guid DestinationAccountId { get; set; }
    public string DestinationAccountName { get; set; }
}