using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Identity
{
    public class User : BaseEntity
    {
        #region Ctor
        User() { }
        public User(string username)
        {
            Username = username;
        }

        public User(string username, string phoneNumber, string email = null, string name = null, string surname = null,
            bool phoneNumberConfirmed = false, bool emailConfirmed = false, string walletId = null)
        {
            Id = Guid.NewGuid().ToString();
            Username = username;
            PhoneNumber = phoneNumber;

            PhoneNumberConfirmed = phoneNumberConfirmed;
            if (!string.IsNullOrEmpty(email))
            {
                Email = email;
                EmailConfirmed = emailConfirmed;
            }
            Name = name;
            Surname = surname;
            JoinedDate = DateTime.Now;
            IsBanned = false;

            if (!string.IsNullOrEmpty(walletId))
                WalletId = walletId;
        }

        #endregion

        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
#nullable enable
        public string? Name { get; set; }
        public string? Surname { get; set; }

        [ForeignKey("Wallet")]
        public string? WalletId { get; set; }
#nullable disable
        public DateTime JoinedDate { get; set; }
        public bool IsBanned { get; set; }

        #region Relation
        public virtual Wallet Wallet { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<Bank> Banks { get; set; }
        public virtual ICollection<UserPermission> Permissions { get; set; }
        #endregion
    }
}