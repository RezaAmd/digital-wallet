namespace DigitalWallet.Domain.Entities.Identity
{
    public class PermissionEntity : BaseEntity
    {
        public string Name { get; set; } // Unique name.
        public DateTime CreatedOn { get; private set; } = DateTime.Now;
        public string Title { get; set; } = null;
        public string Description { get; set; } = null;

        #region Relations

        public virtual ICollection<PermissionRoleEntity> PermissionRoles { get; set; } = null;

        #endregion

        #region Ctor
        PermissionEntity() { }

        public PermissionEntity(string name, string title = null, string description = null)
        {
            Name = name;
            Title = title;
            Description = description;
        }
        #endregion
    }
}