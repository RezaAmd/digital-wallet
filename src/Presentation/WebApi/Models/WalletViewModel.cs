using Domain.Enums;

namespace WebApi.Models
{
    public class WalletDetailVM
    {
        public WalletDetailVM() { }
        public WalletDetailVM(string id, double balance = 0, string bankId = null, string createdDateTime = null)
        {
            Id = id;
            Balance = balance;
            BankId = bankId;
            CreatedDateTime = !string.IsNullOrEmpty(createdDateTime) ? createdDateTime : PersianDateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }
        public string Id { get; set; }
        public double Balance { get; set; }
        public string BankId { get; set; }
        public string CreatedDateTime { get; set; }
    }

    public class GetBalanceVM
    {
        public GetBalanceVM(double balance)
        {
            this.balance = balance;
        }
        public double balance { get; set; }
    }

    public class IncreaseResult
    {
        #region Constructors
        public IncreaseResult(string identify, double amount = 0, string dateTime = null,
            TransferState state = TransferState.Pending, double balance = 0)
        {
            Identify = identify;
            Amount = amount;
            DateTime = dateTime == null ?
                PersianDateTime.Now.ToString("dddd, dd MMMM yyyy") : dateTime;
            State = state;
            Balance = balance;
        }
        #endregion

        public string Identify { get; set; }
        public double Amount { get; set; }
        public string DateTime { get; set; }
        public TransferState State { get; set; }
        public double Balance { get; set; }
    }

    public class DecreaseResult
    {
        #region Constructors
        public DecreaseResult(string identify, double amount = 0, string dateTime = null,
            TransferState state = TransferState.Success, double balance = 0)
        {
            Identify = identify;
            Amount = amount;
            DateTime = dateTime == null ?
                PersianDateTime.Now.ToString("dddd, dd MMMM yyyy") : dateTime;
            State = state;
            Balance = balance;
        }
        #endregion

        public string Identify { get; set; }
        public double Amount { get; set; }
        public string DateTime { get; set; }
        public TransferState State { get; set; }
        public double Balance { get; set; }
    }
}
