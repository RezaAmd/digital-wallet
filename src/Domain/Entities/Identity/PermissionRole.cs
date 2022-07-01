using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalWallet.Domain.Entities.Identity
{
    public class PermissionRole : BaseEntity
    {
        #region Ctor
        PermissionRole() { }

        public PermissionRole(string roleId, string permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
            AssignedDateTime = DateTime.Now;
        }
        #endregion

        public DateTime AssignedDateTime { get; set; }

        [ForeignKey("Role")]
        public string RoleId { get; set; }
        public virtual Role Role { get; set; }

        [ForeignKey("Permission")]
        public string PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
    }
}