using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Bank : BaseEntity
    {
        #region Ctor
        Bank() { }

        /// <summary>
        /// Bank object model.
        /// </summary>
        /// <param name="name">Name of the bank. must be unique.</param>
        /// <param name="title">A title is nullable for display name of bank.</param>
        public Bank(string name, string title = null)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Title = title;
            CreatedDateTime = DateTime.Now;
        }
        #endregion

        public string Id { get; set; }
        public string Name { get; set; } // Unique name.
        public DateTime CreatedDateTime { get; set; }
#nullable enable
        public string? Title { get; set; } // Display name.
#nullable disable

        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}