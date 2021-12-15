using Domain.Entities.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class TransactionHistory
    {
        public string Id { get; set; }
        public double Amount { get; set; }
        public double Balance { get; set; }
        public DateTime DateTime { get; set; }

        [ForeignKey("Origin")]
        public string OriginId { get; set; }
        [ForeignKey("Destination")]
        public string DestinationId { get; set; }

        public virtual User Origin { get; set; }
        public virtual User Destination { get; set; }
    }
}
