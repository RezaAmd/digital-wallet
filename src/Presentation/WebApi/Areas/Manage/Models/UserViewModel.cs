namespace WebApi.Areas.Manage.Models
{
    public class CreateUserVM
    {
        public CreateUserVM(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
    }

    public class UserThumbailVM
    {
        public UserThumbailVM(string id, string username, string phoneNumber, string email, string name, string surname)
        {
            Id = id;
            Username = username;
            PhoneNumber = phoneNumber;
            Email = email;
            Name = name;
            Surname = surname;
        }
        public string Id { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
