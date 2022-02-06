using System;
using System.Collections.Generic;

namespace Domain.Entities.Identity
{
    public class Role : BaseEntity
    {
        #region Ctor
        Role() { }

        public Role(string slug, string name = null,
            string description = null)
        {
            Id = Guid.NewGuid().ToString();
            Slug = slug;
            Name = name;
            Description = description;
            CreatedDateTime = DateTime.Now;
        }
        #endregion

        public string Id { get; set; }
        public string Slug { get; set; }
        public DateTime CreatedDateTime { get; set; }
#nullable enable
        public string? Name { get; set; }
        public string? Description { get; set; }

#nullable disable

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}