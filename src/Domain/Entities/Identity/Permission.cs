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
        public DateTime CreatedDate { get; set; }

#nullable enable
        public string? Name { get; set; }
        public string? Description { get; set; }
#nullable disable

        #region Relations
        public virtual ICollection<PermissionRole> PermissionRoles { get; set; }
        #endregion
    }
}