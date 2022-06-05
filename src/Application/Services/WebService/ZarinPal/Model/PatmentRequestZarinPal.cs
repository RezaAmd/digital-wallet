namespace Application.Services.WebService.ZarinPal.Model
{
    public class PatmentRequestZarinPal : ZarinpalBaseAuthorize
    {
        #region Constructors
        public PatmentRequestZarinPal(double amount = 1000, string description = null,
            string callback = null, string mobile = null, string email = null)
        {
            this.amount = amount;
            this.description = description;
            callback_url = callback;
            this.mobile = mobile;
            this.email = email;
        }
        #endregion

        public double amount { get; set; }
        public string description { get; set; }
        public string callback_url { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
    }
}