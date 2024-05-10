using System;
using System.Collections.Generic;

namespace DigitalWallet.Domain.Entities.Identity
{
    public class RoleEntity : BaseEntity
    {
        #region Ctor
        RoleEntity() { }

        public RoleEntity(string name, string title = null,
            string description = null)
        {
            Name = name;
            Name = title;
            Description = description;
            CreatedDateTime = DateTime.Now;
        }
        #endregion

        public string Name { get; set; }
        public DateTime CreatedDateTime { get; set; }
#nullable enable
        public string? Title { get; set; }
        public string? Description { get; set; }

#nullable disable

        public virtual ICollection<UserRoleEntity> UserRoles { get; set; }
    }
}