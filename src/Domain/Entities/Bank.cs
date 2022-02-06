using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Bank : BaseEntity
    {
        #region Ctor
        Bank() { }

        /// <summary>
        /// Bank object model.
        /// </summary>
        /// <param name="name">Name of the bank</param>
        /// <param name="userId">The user id is owner of this bank.</param>
        public Bank(string name, string userId)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            OwnerId = userId;
            CreatedDateTime = DateTime.Now;
        }
        #endregion

        public string Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDateTime { get; set; }

        [ForeignKey("Owner")]
        public string OwnerId { get; set; }
        public virtual User Owner { get; set; }

        public virtual ICollection<Wallet> Wallets { get; set; }
        public virtual ICollection<PermissionRole> PermissionRoles { get; set; }
    }
}