using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Identity
{
    public class Permission
    {
        #region Ctor
        Permission() { }

        public Permission(string slug, string name = null, string description = null)
        {
            Id = Guid.NewGuid().ToString();
            Slug = slug;
            Name = name;
            Description = description;
            CreatedDate = DateTime.Now;
        }
        #endregion

        public string Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

#nullable enable
        [ForeignKey("Bank")]
        public string? BankId { get; set; }
#nullable disable
        public virtual Bank Bank { get; set; }

        public virtual ICollection<PermissionRole> PermissionRoles { get; set; }
    }
}