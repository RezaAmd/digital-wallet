using Domain.Enums;

namespace WebApi.Areas.Manage.Models
{
    public class DepositMVM
    {
        public string TraceId { get; set; }
        public double amount { get; set; }
        public string destination { get; set; }
        public DepositState state { get; set; }
        public string dateTime { get; set; }
    }
}
