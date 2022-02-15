using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class CreateWalletDto
    {
        [Required(ErrorMessage = "مقدار اولیه نمیتواند خالی باشد.")]
        public string Seed { get; set; }

    }

    public class IncreaseDto
    {
        public string WalletId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
    }

    public class DecreaseDto
    {
        public string WalletId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
    }
}
