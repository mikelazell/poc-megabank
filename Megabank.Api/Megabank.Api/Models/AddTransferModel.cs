using System.ComponentModel.DataAnnotations;

namespace Megabank.Api.Models;

public class AddTransferModel
{
    public string Reference { get; set; }
    public decimal Value { get; set; }
    public Guid SourceAccountId { get; set; }
    public int DestinationSortCode { get; set; }
    public int DestinationAccountNumber { get; set; }
}