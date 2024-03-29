﻿using DigitalWallet.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalWallet.Domain.Entities.Identity
{
    public class UserRole : BaseEntity
    {
        public DateTime AssignedDateTime { get; set; }
        public RelatedPermissionType Type { get; set; }
#nullable enable
        public string? RelatedToId { get; set; }
#nullable disable
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Role")]
        public string RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}