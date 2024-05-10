using DigitalWallet.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalWallet.Domain.Entities.Identity
{
    public class UserPermissionEntity : BaseEntity
    {
        #region Ctor

        UserPermissionEntity() { }

        public UserPermissionEntity(Guid userId, Guid permissionId,
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
        public Guid? RelatedToId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual UserEntity User { get; set; }

        [ForeignKey("Permission")]
        public Guid PermissionId { get; set; }
        public virtual PermissionEntity Permission { get; set; }
    }
}