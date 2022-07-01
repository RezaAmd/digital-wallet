using System.ComponentModel.DataAnnotations;

namespace WebApi.Areas.Manage.Models;

public class CreatePermissionMDto
{
    [Required(ErrorMessage = "وارد کردن نام ضروری است.")]
    public string name { get; set; }
#nullable enable
    public string? title { get; set; }
    public string? description { get; set; }
#nullable disable
    public List<string> rolesId { get; set; } // List of role id.
}
