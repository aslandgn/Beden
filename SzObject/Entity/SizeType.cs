using System.ComponentModel.DataAnnotations.Schema;

namespace SzObject.Entity
{
    [Table("SizeType")]
    public class SizeType
    {
        public string Name { get; set; }
    }
}
