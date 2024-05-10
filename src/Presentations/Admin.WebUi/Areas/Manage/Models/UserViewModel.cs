namespace DigitalWallet.Admin.WebUi.Areas.Manage.Models;

public class CreateUserMVM
{
    public CreateUserMVM(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}

public class UserThumbailMVM
{
    public UserThumbailMVM(Guid id, string phoneNumber,
        string email, string? name, string? surname)
    {
        Id = id;
        PhoneNumber = phoneNumber;
        Email = email;
        Name = name;
        Surname = surname;
    }

    public Guid Id { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}