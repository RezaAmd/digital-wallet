using System;

namespace Domain.Entities
{
    public class Wallet
    {
        public string Id { get; set; }
        public string Seed { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}