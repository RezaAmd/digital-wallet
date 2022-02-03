using Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Identity
{
    public class Role
    {
        #region Constructors
        Role() { }

        public Role(string slug, string name = null,
            string description = null, RoleType type = RoleType.General)
        {
            Id = Guid.NewGuid().ToString();
            Slug = slug;
            Name = name;
            Description = description;
            Type = type;
            CreatedDateTime = DateTime.Now;
        }
        #endregion

        public string Id { get; set; }
        public string Slug { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public RoleType Type { get; set; }
#nullable enable
        public string? Name { get; set; }
        public string? Description { get; set; }

        [ForeignKey("Bank")]
        public string? BankId { get; set; }
#nullable disable

        public virtual Bank Bank { get; set; }
    }
}