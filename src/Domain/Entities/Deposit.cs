﻿using Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Deposit
    {
        public string Id { get; set; }
        public string TraceId { get; set; } // Transaction id, identity, identify, refence or ...
        public double Amount { get; set; }
        public DateTime DateTime { get; set; }
        public DepositState State { get; set; }

        [ForeignKey("Wallet")]
        public string DestinationId { get; set; }
        public virtual Wallet Wallet { get; set; }
    }
}