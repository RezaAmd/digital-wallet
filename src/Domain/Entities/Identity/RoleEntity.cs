namespace DigitalWallet.Domain.Entities.Identity
{
    public class RoleEntity : BaseEntity
    {
        public string Name { get; set; }
        public DateTime CreatedOn { get; private set; } = DateTime.Now;
        public string? Title { get; set; }
        public string? Description { get; set; }

        #region Relations

        public virtual ICollection<UserRoleEntity> UserRoles { get; private set; }
        public virtual ICollection<PermissionRoleEntity> PermissionRoles { get; private set; }

        #endregion

        #region Ctor

        RoleEntity() { }
        public RoleEntity(string name, string title = null,
            string description = null)
        {
            Name = name;
            Name = title;
            Description = description;
        }

        #endregion
    }
}