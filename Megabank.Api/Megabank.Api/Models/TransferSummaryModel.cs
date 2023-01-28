namespace Megabank.Api.Models;

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

    public static TransferSummaryModel FromTransferModel(Guid viewingAccountId, TransferModel transferModel)
    {
        return new TransferSummaryModel
        {
            Date = transferModel.Date,
            DestinationAccountId = transferModel.DestinationAccountId,
            Id = transferModel.Id,
            Reference = transferModel.Reference,
            SourceAccountId = transferModel.SourceAccountId,
            Value = transferModel.Value * (transferModel.DestinationAccountId == viewingAccountId ? 1 : -1)
        };
    }
}