using DigitalWallet.Domain.Enums;

namespace DigitalWallet.Domain.Entities.Identity
{
    public class UserPermissionEntity : BaseEntity
    {
        public DateTime CreatedOn { get; private set; } = DateTime.Now;
        public RelatedPermissionType Type { get; private set; }
        public Guid? RelatedToId { get; private set; }
        public Guid UserId { get; private set; }
        public Guid PermissionId { get; private set; }

        #region Relations

        public virtual UserEntity User { get; private set; }
        public virtual PermissionEntity Permission { get; private set; }

        #endregion

        #region Ctor

        UserPermissionEntity() { }

        public UserPermissionEntity(Guid userId, Guid permissionId,
            RelatedPermissionType type = RelatedPermissionType.General)
        {
            UserId = userId;
            PermissionId = permissionId;
            Type = type;
        }

        #endregion
    }
}