using BackendTemplate.Domain.Core.Interfaces;
using BackendTemplate.Domain.DTO.UsuarioDTOs;
using BackendTemplate.Domain.Entities;
using System.Threading.Tasks;

namespace BackendTemplate.Domain.Interfaces.UsuarioInterfaces
{
    public interface IUsuarioRepository :
        IBaseRepository,
        IRepositorySelect<Usuario>,
        IRepositorySelectByHash<Usuario>,
        IRepositorySelectById<Usuario>,
        IRepositorySelectPaged<Usuario>,
        IRepositoryUpdate<Usuario>,
        IRepositoryInsert<Usuario>
    {
        public Task<TDTO> Select<TDTO>(UsuarioLoginRequest usuarioRequest);
    }
}
