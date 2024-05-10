using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalWallet.Domain.Entities.Identity
{
    public class UserEntity : BaseEntity
    {
        #region Ctor
        UserEntity() { }
        public UserEntity(string username)
        {
            Username = username;
        }
        public UserEntity(string username, string phoneNumber, string email = null, string name = null, string surname = null,
            bool phoneNumberConfirmed = false, bool emailConfirmed = false, Guid? walletId = null)
        {
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

            if (walletId.HasValue)
                WalletId = walletId;
        }

        #endregion

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
        public Guid? WalletId { get; set; }
#nullable disable
        public DateTime JoinedDate { get; set; }
        public bool IsBanned { get; set; }

        #region Relation
        public virtual WalletEntity? Wallet { get; set; } = null;
        public virtual ICollection<UserRoleEntity>? UserRoles { get; set; } = null;
        public virtual ICollection<BankEntity>? Banks { get; set; } = null;
        public virtual ICollection<UserPermissionEntity>? Permissions { get; set; } = null;
        #endregion
    }
}