using System.ComponentModel.DataAnnotations;

namespace DigitalWallet.WebApi.Areas.Manage.Models;

public class CreateUserMDto
{

    [Required(ErrorMessage = "رمز ورود اجباری میباشد.")]
    [Phone(ErrorMessage = "شماره همراه را به درستی وارد کنید.")]
    [StringLength(11, ErrorMessage = "شماره همراه باید 11 رقم باشد.", MinimumLength = 11)]
    public string phoneNumber { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "ایمیل را به درستی وارد کنید.")]
    public string email { get; set; } = string.Empty;
    [Required(ErrorMessage = "رمز ورود اجباری میباشد.")]
    [StringLength(20, ErrorMessage = "رمز ورود باید بین 4 تا 20 کارکتر باشد.", MinimumLength = 4)]
    public string password { get; set; } = string.Empty;

    [StringLength(20, ErrorMessage = "نام باید بین 3 تا 20 کارکتر باشد.", MinimumLength = 3)]
    public string name { get; set; } = string.Empty;

    [StringLength(20, ErrorMessage = "نام خانوادگی باید بین 3 تا 20 کارکتر باشد.", MinimumLength = 3)]
    public string surname { get; set; } = string.Empty;
}

public class EditUserMDto
{
    public string username { get; set; } = string.Empty;
    public string phoneNumber { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public string surname { get; set; } = string.Empty;
}