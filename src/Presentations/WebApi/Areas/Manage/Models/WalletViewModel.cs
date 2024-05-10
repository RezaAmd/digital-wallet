namespace DigitalWallet.WebApi.Areas.Manage.Models;

public class WalletViewModelManage
{
    public string seed { get; set; }
    public string createdDateTime { get; set; }
    public OwnerViewModelManage owner { get; set; }
}

public class OwnerViewModelManage
{
    public Guid id { get; set; }
    public string name { get; set; }
    public string surname { get; set; }
}