namespace DigitalWallet.WebApi.Models;

public class SignInVM
{
    public SignInVM(string token)
    {
        this.token = token;
    }
    public string token { get; set; }
}