using BackendTemplate.Domain.Core.Entities;

namespace BackendTemplate.Domain.Entities
{
    public class UsuarioPerfil : Entity
    {        
        public virtual int UsuarioId { get; set; }
        public virtual int PerfilId { get; set; }
    }
}

