using BackendTemplate.Domain.Core.Interfaces;
using BackendTemplate.Domain.Entities;

namespace BackendTemplate.Domain.Interfaces.PerfilInterfaces
{
    public interface IPerfilRepository :
        IBaseRepository,
        IRepositorySelect<Perfil>,
        IRepositorySelectById<Perfil>
    {
    }
}
