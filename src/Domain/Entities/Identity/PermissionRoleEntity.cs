namespace DigitalWallet.Domain.Entities.Identity
{
    public class PermissionRoleEntity : BaseEntity
    {
        public DateTime CreatedOn { get; private set; } = DateTime.Now;
        public Guid RoleId { get; private set; }
        public Guid PermissionId { get; private set; }

        #region Relations

        public virtual RoleEntity Role { get; private set; }
        public virtual PermissionEntity Permission { get; private set; }

        #endregion

        #region Ctor

        PermissionRoleEntity() { }
        public PermissionRoleEntity(Guid roleId, Guid permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }

        #endregion
    }
}