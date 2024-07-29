using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTemplate.Domain.Core.Entities
{
    public abstract class GeneralEntity : Entity
    {
        public GeneralEntity()
        {
            this.EntityControl = new EntityControl();
        }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Hash { get; set; }

        [Required]
        public virtual EntityControl EntityControl { get; set; }
    }
}
