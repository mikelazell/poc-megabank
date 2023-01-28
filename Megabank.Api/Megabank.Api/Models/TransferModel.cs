namespace Megabank.Api.Models;

public class TransferModel
{
    public TransferModel()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; init; }
    public DateTime Date { get; set; }
    public string Reference { get; set; }
    public decimal Value { get; set; }
    public Guid SourceAccountId { get; set; }
    public Guid DestinationAccountId { get; set; }
}