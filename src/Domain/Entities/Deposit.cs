namespace Domain.Entities
{
    public class Deposit
    {
        public string Id { get; set; }
        public string TraceId { get; set; }
        public double Amount { get; set; }
        public string WalletId { get; set; }
        public int State { get; set; }
    }
}