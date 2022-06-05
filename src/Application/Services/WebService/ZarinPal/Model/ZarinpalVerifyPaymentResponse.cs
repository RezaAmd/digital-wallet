namespace Application.Services.WebService.ZarinPal.Model
{
    public class ZarinpalVerifyPaymentResponse
    {
        public int code { get; set; }
        public long ref_id { get; set; }
        public string card_pan { get; set; }
        public string card_hash { get; set; }
        public string fee_type { get; set; }
        public double fee { get; set; }
    }
}