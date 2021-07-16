using BackendTemplate.Domain.Core.DTO;
using BackendTemplate.Domain.Core.Interfaces;
using BackendTemplate.Domain.DTO.UsuarioDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendTemplate.Domain.Interfaces.UsuarioInterfaces
{
    public interface IUsuarioSelectService : IBaseService
    {
        Task<ServiceResult<UsuarioResponse>> Select(UsuarioLoginRequest usuarioRequest);
        Task<ServiceResult<ICollection<UsuarioResponse>>> Select();
    }
}