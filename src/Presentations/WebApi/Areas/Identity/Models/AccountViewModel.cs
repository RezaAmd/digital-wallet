namespace WebApi.Areas.Identity.Models;

public class SignInVM
{
    public SignInVM(string token)
    {
        this.token = token;
    }
    public string token { get; set; }
}