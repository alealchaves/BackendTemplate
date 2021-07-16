using BackendTemplate.Domain.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BackendTemplate.Domain.Entities
{
    public class Perfil : GeneralEntity
    {
        [Required]
        [MaxLength(500)]
        public virtual string Nome { get; set; }

        public virtual ICollection<UsuarioPerfil> UsuarioPerfis { get; set; }
    }
}

