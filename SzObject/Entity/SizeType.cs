using System.ComponentModel.DataAnnotations.Schema;

namespace SzObject.Entity
{
    [Table("SizeType")]
    public class SizeType : SharedEntity
    {
        public string Name { get; set; }
    }
}
