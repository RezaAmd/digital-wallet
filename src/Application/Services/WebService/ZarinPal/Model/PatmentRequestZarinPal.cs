namespace Infrastructure.Common.Services.WebService.ZarinPal.Model
{
    public class PatmentRequestZarinPal
    {
        public PatmentRequestZarinPal(double amount = 1000, string description = "پرداخت تستی",
            string callback = null, string mobile = null)
        {
            Merchant_id = "c581ad20-7ef9-4553-ae6f-6f175c39af9e";
            Amount = amount;
            Description = description;
            Callback_url = callback;
            Mobile = mobile;
        }
        public string Merchant_id { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public string Callback_url { get; set; }
        public string Mobile { get; set; }
    }
}