﻿namespace WebApi.Areas.Manage.Models
{
    public class WalletViewModelManage
    {
        public string Seed { get; set; }
        public string createdDateTime { get; set; }
        public OwnerViewModelManage owner { get; set; }
    }

    public class OwnerViewModelManage
    {
        public string id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
    }
}