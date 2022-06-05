namespace Application.Services.WebService.ZarinPal.Model
{
    public class ZarinpalBaseAuthorize
    {
        public ZarinpalBaseAuthorize()
        {
            this.merchant_id = "c581ad20-7ef9-4553-ae6f-6f175c39af9e";
        }
        public string merchant_id { get; private set; }
    }
}