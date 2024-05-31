using System.ComponentModel.DataAnnotations;

namespace DigitalWallet.Admin.WebUi.Areas.Manage.Models;

public class CreateWalletMDto
{
    [Required(ErrorMessage = "انتخاب یک مقدار اولیه ضروری است.")]
    public string seed { get; set; } = string.Empty;
    public Guid? masterWalletId { get; set; } = null;
}