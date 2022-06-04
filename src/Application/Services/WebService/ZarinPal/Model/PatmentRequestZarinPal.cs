namespace Application.Services.WebService.ZarinPal.Model
{
    public class PatmentRequestZarinPal
    {
        #region Constructors
        public PatmentRequestZarinPal(double amount = 1000, string description = "پرداخت تستی",
            string callback = null, string mobile = null, string email = null)
        {
            merchant_id = "c581ad20-7ef9-4553-ae6f-6f175c39af9e";
            this.amount = amount;
            this.description = description;
            callback_url = callback;
            this.mobile = mobile;
            this.email = mobile;
        }
        #endregion

        public string merchant_id { get; private set; }
        public double amount { get; set; }
        public string description { get; set; }
        public string callback_url { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
    }
}