using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Identity
{
    public class Permission : BaseEntity
    {
        #region Ctor
        Permission() { }

        public Permission(string name, string title = null, string description = null)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Title = title;
            Description = description;
            CreatedDate = DateTime.Now;
        }
        #endregion

        public string Id { get; set; }
        public string Name { get; set; } // Unique name.
        public DateTime CreatedDate { get; set; }
#nullable enable
        public string? Title { get; set; }
        public string? Description { get; set; }
#nullable disable

        public virtual ICollection<PermissionRole> PermissionRoles { get; set; }
    }
}