using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Identity
{
    public class UserPermission
    {
        #region Ctor
        UserPermission() { }

        public UserPermission(string userId, string permissionId)
        {

        }
        #endregion

        public DateTime AssignedDateTime { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Permission")]
        public string PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
