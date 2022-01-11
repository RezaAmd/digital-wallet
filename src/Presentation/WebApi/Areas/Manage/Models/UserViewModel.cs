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
        public string id { get; set; }
        public string username { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
    }
}
