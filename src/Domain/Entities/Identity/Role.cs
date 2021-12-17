using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Identity
{
    public class Role
    {
        #region Constructors
        Role() { }
        public Role(string name)
        {
            Id = Guid.NewGuid().ToString();
        }
        #endregion

        public string Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Parent")]
        public string ParentId { get; set; }
        public virtual Role Parent { get; set; }

        public virtual ICollection<Role> Children { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}