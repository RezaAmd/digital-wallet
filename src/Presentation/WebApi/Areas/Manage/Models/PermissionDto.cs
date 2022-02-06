using System.ComponentModel.DataAnnotations;

namespace WebApi.Areas.Manage.Models
{
    public class CreatePermissionMDto
    {
        [Required(ErrorMessage = "یک نام وارد کنید.")]
        public string slug { get; set; }
#nullable enable
        public string? name { get; set; }
        public string? description { get; set; }
#nullable disable
    }
}
