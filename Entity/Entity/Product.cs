using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Object.Entity
{
    [Table("Product")]
    public class Product : SharedEntity
    {
        public Guid CategoryGuid { get; set; }
        public Guid SizeTypeGuid { get; set; }
        public string Name { get; set; }
    }
}
