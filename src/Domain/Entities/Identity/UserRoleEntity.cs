using DigitalWallet.Domain.Enums;

namespace DigitalWallet.Domain.Entities.Identity
{
    public class UserRoleEntity : BaseEntity
    {
        public DateTime AssignedDateTime { get; set; }
        public RelatedPermissionType Type { get; set; }
        public Guid? RelatedToId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public virtual UserEntity User { get; private set; } = null;
        public virtual RoleEntity Role { get; private set; } = null;
    }
}