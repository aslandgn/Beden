using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Object.Entity
{
    [Table("Category")]
    public class Category: SharedEntity
    {
        public Guid? ParentCategoryGuid { get; set; }
        public string Name { get; set; }

        public Category() { }
        public Category(string name, Guid? parentId)
        {
            Guid = Guid.NewGuid();
            Name = name;
            ParentCategoryGuid = parentId;
            Status = true;
            IsDeleted = false;
        }
    }
}
