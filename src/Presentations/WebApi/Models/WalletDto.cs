using System.ComponentModel.DataAnnotations;

namespace DigitalWallet.WebApi.Models;

public class CreateWalletDto
{
    [Required(ErrorMessage = "Seed of wallet cannot be null.")]
    public string Seed { get; set; }

}

public class IncreaseDto
{
    [Required(ErrorMessage = "Wallet identity cannot be null.")]
    public Guid WalletId { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; } = null;
}

public class DecreaseDto
{
    [Required(ErrorMessage = "Wallet identity cannot be null.")]
    public Guid WalletId { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; } = null;
}

public class DepositDto
{
    [Required(ErrorMessage = "Wallet identity cannot be null.")]
    public Guid WalletId { get; set; }
    [Range(100, 500000, ErrorMessage = "Amount must greater then {1} and smaller than 500,000.")]
    public decimal Amount { get; set; }
    [Required(ErrorMessage = "Trace id cannot be null.")]
    public string? TraceId { get; set; }
    [Required(ErrorMessage = "Callback cannot be null.")]
    public string? Callback { get; set; }
    public string? Description { get; set; } = null;
    public string? Mobile { get; set; } = null;
    public string? Email { get; set; } = null;
}