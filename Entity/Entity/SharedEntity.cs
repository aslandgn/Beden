using Core.Interface;
using System;
using System.ComponentModel.DataAnnotations;

namespace Object.Entity
{
    public abstract class SharedEntity: IEntity
    {
        [Key]
        public Guid Guid { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
