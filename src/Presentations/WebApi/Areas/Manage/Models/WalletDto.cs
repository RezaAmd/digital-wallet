using System.ComponentModel.DataAnnotations;

namespace DigitalWallet.WebApi.Areas.Manage.Models;

public class CreateWalletMDto
{
    [Required(ErrorMessage = "انتخاب یک مقدار اولیه ضروری است.")]
    public string seed { get; set; } = string.Empty;
    public Guid? safeId { get; set; } = null;
}