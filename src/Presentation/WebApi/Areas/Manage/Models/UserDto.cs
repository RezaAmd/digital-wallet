using System.ComponentModel.DataAnnotations;

namespace WebApi.Areas.Manage.Models
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "نام کاربری اجباری میباشد.")]
        [StringLength(20, ErrorMessage = "نام کاربری باید بین 3 تا 20 کارکتر باشد.", MinimumLength = 3)]
        public string username { get; set; }

        [Required(ErrorMessage = "رمز ورود اجباری میباشد.")]
        [StringLength(20, ErrorMessage = "رمز ورود باید بین 4 تا 20 کارکتر باشد.", MinimumLength = 4)]
        public string password { get; set; }

        [Required(ErrorMessage = "رمز ورود اجباری میباشد.")]
        [Phone(ErrorMessage = "شماره همراه را به درستی وارد کنید.")]
        [StringLength(11, ErrorMessage = "شماره همراه باید 11 رقم باشد.", MinimumLength = 11)]
        public string phoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "ایمیل را به درستی وارد کنید.")]
        public string email { get; set; }

        [StringLength(20, ErrorMessage = "نام باید بین 3 تا 20 کارکتر باشد.", MinimumLength = 3)]
        public string name { get; set; }

        [StringLength(20, ErrorMessage = "نام خانوادگی باید بین 3 تا 20 کارکتر باشد.", MinimumLength = 3)]
        public string surname { get; set; }
    }

    public class EditUserDto
    {
        public string username { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
    }
}
