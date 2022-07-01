using System;
using System.Collections.Generic;

namespace DigitalWallet.Domain.Entities.Identity
{
    public class Role : BaseEntity
    {
        #region Ctor
        Role() { }

        public Role(string name, string title = null,
            string description = null)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Name = title;
            Description = description;
            CreatedDateTime = DateTime.Now;
        }
        #endregion

        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDateTime { get; set; }
#nullable enable
        public string? Title { get; set; }
        public string? Description { get; set; }

#nullable disable

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}