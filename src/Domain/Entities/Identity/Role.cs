using System;

namespace Domain.Entities.Identity
{
    public class Role
    {
        #region Constructors
        Role() { }
        public Role(string slug, string name = null,
            string description = null, bool isGeneric = true)
        {
            Id = Guid.NewGuid().ToString();
            Slug = slug;
            Name = name;
            Description = description;
            IsGeneric = isGeneric;
            CreatedDateTime = DateTime.Now;
        }
        #endregion

        public string Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public bool IsGeneric { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}