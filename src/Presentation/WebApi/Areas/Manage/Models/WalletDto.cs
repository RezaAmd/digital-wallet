using System.ComponentModel.DataAnnotations;

namespace WebApi.Areas.Manage.Models
{
    public class CreateWalletDto
    {
        [Required(ErrorMessage = "انتخاب یک مقدار اولیه ضروری است.")]
        public string seed { get; set; }
#nullable enable
        public string? bankId { get; set; }
#nullable disable
    }
}
