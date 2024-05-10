using System;
using System.Collections.Generic;

namespace DigitalWallet.Domain.Entities.Identity
{
    public class PermissionEntity : BaseEntity
    {
        #region Ctor
        PermissionEntity() { }

        public PermissionEntity(string name, string title = null, string description = null)
        {
            Name = name;
            Title = title;
            Description = description;
            CreatedDate = DateTime.Now;
        }
        #endregion

        public string Name { get; set; } // Unique name.
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; } = null;
        public string Description { get; set; } = null;

        public virtual ICollection<PermissionRoleEntity>? PermissionRoles { get; set; } = null;
    }
}