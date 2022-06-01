namespace Application.Services.WebService.ZarinPal.Model
{
    public class PaymentRequestResultZarinPal
    {
        public int code { get; set; }
        public string message { get; set; }
        public string authority { get; set; }
        public string fee_type { get; set; }
        public int fee { get; set; }
    }
}