using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Identity
{
    public class User
    {
        #region Constructors
        User() { }
        public User(string username)
        {
            UserName = username;
        }

        public User(string username, string phoneNumber)
        {
            UserName = username;
            PhoneNumber = phoneNumber;

            JoinedDate = DateTime.Now;
            IsBanned = false;
        }

        #endregion

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
#nullable enable
        public string? Name { get; set; }
        public string? Surname { get; set; }
#nullable disable
        public DateTime JoinedDate { get; set; }
        public bool IsBanned { get; set; }

        #region Relation
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<Bank> Banks { get; set; }
        #endregion
    }
}