namespace  DigitalWallet.Application.Services.WebService.ZarinPal.Model
{
    public class ZarinpalPaymentVerifyParams : ZarinpalBaseAuthorize
    {
        #region Constructors
        public ZarinpalPaymentVerifyParams(string authority, double amount)
        {
            this.authority = authority;
            this.amount = amount;
        }
        #endregion

        public double amount { get; set; }
        public string authority { get; set; }
    }
}