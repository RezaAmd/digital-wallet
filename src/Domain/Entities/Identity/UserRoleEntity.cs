using DigitalWallet.Domain.Enums;

namespace DigitalWallet.Domain.Entities.Identity
{
    public class UserRoleEntity : BaseEntity
    {
        public DateTime CreatedOn { get; private set; } = DateTime.Now;
        public RelatedPermissionType Type { get; set; }
        public Guid? RelatedToId { get; set; }
        public Guid UserId { get; private set; }
        public Guid RoleId { get; private set; }

        #region Relations

        public virtual UserEntity User { get; private set; } = null;
        public virtual RoleEntity Role { get; private set; } = null;

        #endregion

        #region Ctors

        UserRoleEntity() { }
        public UserRoleEntity(RelatedPermissionType type, Guid userId, Guid roleId)
        {
            Type = type;
            UserId = userId;
            RoleId = roleId;
        }

        #endregion
    }
}