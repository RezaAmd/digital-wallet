using System;

namespace Domain.Entities
{
    public class BankPermission
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AssignedTo { get; set; }
    }
}