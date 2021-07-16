using BackendTemplate.Domain.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BackendTemplate.Domain.Entities
{
    public class Usuario : GeneralEntity
    {
        [Required]
        [MaxLength(500)]
        public virtual string Nome { get; set; }

        [Required]
        [MaxLength(100)]
        public virtual string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public virtual string Senha { get; set; }

        [Required]
        [MaxLength(14)]
        public virtual string Cpf { get; set; }

        public virtual ICollection<UsuarioPerfil> UsuarioPerfis { get; set; }
    }
}

