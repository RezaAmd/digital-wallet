using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class CreateWalletDto
    {
        [Required(ErrorMessage = "Seed of wallet cannot be null.")]
        public string Seed { get; set; }

    }

    public class IncreaseDto
    {
        [Required(ErrorMessage = "Wallet identity cannot be null.")]
        public string WalletId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
    }

    public class DecreaseDto
    {
        [Required(ErrorMessage = "Wallet identity cannot be null.")]
        public string WalletId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
    }

    public class DepositDto
    {
        [Required(ErrorMessage = "Wallet identity cannot be null.")]
        public string WalletId { get; set; }
        private double _amount { get; set; }
        [Range(100, 500000, ErrorMessage = "Amount must greater then {1} and smaller than 500,000.")]
        public double Amount { get { return _amount; } set { _amount = value * 10; } }
        public string Description { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}