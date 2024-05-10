using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalWallet.Domain.Entities.Identity
{
    public class PermissionRoleEntity : BaseEntity
    {
        #region Ctor
        PermissionRoleEntity() { }

        public PermissionRoleEntity(Guid roleId, Guid permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
            AssignedDateTime = DateTime.Now;
        }
        #endregion

        public DateTime AssignedDateTime { get; set; }

        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        public virtual RoleEntity Role { get; set; }

        [ForeignKey("Permission")]
        public Guid PermissionId { get; set; }
        public virtual PermissionEntity Permission { get; set; }
    }
}