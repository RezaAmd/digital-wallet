using DigitalWallet.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalWallet.Domain.Entities.Identity
{
    public class UserRoleEntity : BaseEntity
    {
        public DateTime AssignedDateTime { get; set; }
        public RelatedPermissionType Type { get; set; }
#nullable enable
        public string? RelatedToId { get; set; }
#nullable disable
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual UserEntity User { get; set; }

        [ForeignKey("Role")]
        public string RoleId { get; set; }
        public virtual RoleEntity Role { get; set; }
    }
}