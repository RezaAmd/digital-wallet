using System;

namespace WebApi.Models
{
    public class NewWalletVM
    {
        public NewWalletVM(string walletId, string bankId = null, string createdDateTime = null)
        {
            WalletId = walletId;
            BankId = bankId;
            CreatedDateTime = !string.IsNullOrEmpty(createdDateTime) ? createdDateTime : DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }
        public string WalletId { get; set; }
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
        public string TransactionId { get; set; }
        public double Amount { get; set; }
        public string DateTime { get; set; }
        public int State { get; set; }
        public double Balance { get; set; }
    }
    public class DecreaseResult
    {
        public string TransactionId { get; set; }
        public double Amount { get; set; }
        public string DateTime { get; set; }
        public int State { get; set; }
        public double Balance { get; set; }
    }
}
