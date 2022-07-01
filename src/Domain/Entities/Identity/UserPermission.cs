using DigitalWallet.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalWallet.Domain.Entities.Identity
{
    public class UserPermission : BaseEntity
    {
        #region Ctor
        UserPermission() { }

        public UserPermission(string userId, string permissionId,
            RelatedPermissionType type = RelatedPermissionType.General)
        {
            UserId = userId;
            PermissionId = permissionId;
            AssignedDateTime = DateTime.Now;
            Type = type;
        }
        #endregion

        public DateTime AssignedDateTime { get; set; }
        public RelatedPermissionType Type { get; set; }
#nullable enable
        public string? RelatedToId { get; set; }
#nullable disable

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Permission")]
        public string PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
    }
}