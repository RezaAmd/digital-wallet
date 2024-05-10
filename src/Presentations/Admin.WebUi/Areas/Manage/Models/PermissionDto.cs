using System.ComponentModel.DataAnnotations;

namespace DigitalWallet.Admin.WebUi.Areas.Manage.Models;

public class CreatePermissionMDto
{
    [Required(ErrorMessage = "وارد کردن نام ضروری است.")]
    public string? name { get; set; } = null;
#nullable enable
    public string? title { get; set; } = null;
    public string? description { get; set; } = null;
#nullable disable
    public List<Guid> rolesId { get; set; } // List of role id.
}
