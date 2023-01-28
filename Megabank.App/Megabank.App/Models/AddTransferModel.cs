using System.ComponentModel.DataAnnotations;

namespace Megabank.App.Models;

public class AddTransferModel
{
    [Required]
    [StringLength(30, ErrorMessage = "Reference too long (30 character limit).")]
    public string Reference { get; set; }

    [Required]
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Please enter a valid amount")]
    [Range(0, 10000, ErrorMessage = "Only transfer up to £10,000 are supported")]
    public decimal Value { get; set; }

    [Required(ErrorMessage = "Please select an account to transfer money to")]
    public Guid? SourceAccountId { get; set; }

    [Required(ErrorMessage = "Please enter a destination account sort code")]
    public int DestinationSortCode { get; set; }

    [Required(ErrorMessage = "Please enter a destination account number")]
    public int DestinationAccountNumber { get; set; }
}