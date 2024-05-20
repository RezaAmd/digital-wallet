using DigitalWallet.Domain.Enums;

namespace DigitalWallet.WebApi.Models;

public class WalletDetailVM
{
    public WalletDetailVM() { }
    public WalletDetailVM(Guid id, decimal balance = 0, string safeId = null, string createdDateTime = null)
    {
        Id = id;
        Balance = balance;
        SafeId = safeId;
        CreatedDateTime = !string.IsNullOrEmpty(createdDateTime) ? createdDateTime : PersianDateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
    }
    public Guid Id { get; set; }
    public decimal Balance { get; set; }
    public string? SafeId { get; set; } = null;
    public string CreatedDateTime { get; set; }
}

public class GetBalanceVM
{
    public GetBalanceVM(decimal balance)
    {
        if (balance <= 0)
        {
            Balance = 0;
        }
        else
            Balance = balance;
    }

    public decimal Balance { get; set; }
}

public class IncreaseResult
{
    #region Constructors
    public IncreaseResult(string identify, decimal amount = 0, string dateTime = null,
        TransferState state = TransferState.Failed, decimal? originBalance = null, decimal? destinationBalance = null)
    {
        Identify = identify;
        Amount = amount;
        DateTime = dateTime == null ?
            PersianDateTime.Now.ToString("dddd, dd MMMM yyyy") : dateTime;
        State = state;
        OriginBalance = originBalance;
        DestinationBalance = destinationBalance;
    }
    #endregion

    public string Identify { get; set; }
    public decimal Amount { get; set; }
    public string DateTime { get; set; }
    public TransferState State { get; set; }
    public decimal? OriginBalance { get; set; }
    public decimal? DestinationBalance { get; set; }
}

public class DecreaseResult
{
    #region Constructors
    public DecreaseResult(string identify, decimal amount = 0, string dateTime = null,
        TransferState state = TransferState.Success, decimal? originBalance = null, decimal? destinationBalance = null, string description = null)
    {
        Identify = identify;
        Amount = amount;
        DateTime = dateTime == null ?
            PersianDateTime.Now.ToString("dddd, dd MMMM yyyy") : dateTime;
        State = state;
        OriginBalance = originBalance;
        DestinationBalance = destinationBalance;
        Description = description;
    }
    #endregion

    public string Identify { get; set; }
    public decimal Amount { get; set; }
    public string DateTime { get; set; }
    public TransferState State { get; set; }
    public decimal? OriginBalance { get; set; }
    public decimal? DestinationBalance { get; set; }
    public string Description { get; set; }
}

public class DepositVM
{
    #region Constructors
    public DepositVM(string getwayLink)
    {
        GetwayLink = getwayLink;
    }
    #endregion

    public string GetwayLink { get; set; }
}