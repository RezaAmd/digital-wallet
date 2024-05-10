using System;
using System.Collections.Generic;

namespace DigitalWallet.Domain.Entities
{
    public class SafeEntity : BaseEntity
    {
        public string Name { get; set; }
        public string ApiKey { get; set; }
        public DateTime CreatedDateTime { get; private set; } = DateTime.Now;
        public virtual ICollection<WalletEntity> Wallets { get; set; } = null;

        #region Ctor

        SafeEntity() { }
        /// <summary>
        /// Bank object model.
        /// </summary>
        /// <param name="name">Name of the bank. must be unique.</param>
        /// <param name="title">A title is nullable for display name of bank.</param>
        public SafeEntity(string name, string apiKey)
        {
            Name = name;
            ApiKey = apiKey;
        }

        #endregion
    }
}