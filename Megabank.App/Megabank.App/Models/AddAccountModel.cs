using System.ComponentModel.DataAnnotations;

namespace Megabank.App.Models
{
    public class AddAccountModel
    {
        public AddAccountModel()
        {
        }
        [Required]
        [StringLength(30, ErrorMessage = "Account name too long (30 character limit).")]
        public string Name { get; set; }
    }
}
