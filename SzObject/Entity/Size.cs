using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SzObject.Entity
{
    [Table("Size")]
    public class Size : SharedEntity
    {
        public Guid SizeTypeGuid { get; set; }
        public string Name { get; set; }
        public short Order { get; set; }
    }
}
