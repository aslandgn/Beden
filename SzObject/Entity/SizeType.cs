using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SzObject.Entity
{
    [Table("SizeType")]
    public class SizeType : SharedEntity
    {
        public string Name { get; set; }

        public SizeType() { }
        public SizeType(string name)
        {
            Guid = Guid.NewGuid();
            Name = name;
            Status = true;
            IsDeleted = false;
        }
    }
}
