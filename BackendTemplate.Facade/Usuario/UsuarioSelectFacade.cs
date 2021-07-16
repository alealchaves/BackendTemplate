using BackendTemplate.Domain.Core.DTO;
using BackendTemplate.Domain.DTO.UsuarioDTOs;
using BackendTemplate.Domain.Interfaces.UsuarioInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarramentoPedagogico.Application.Usuario
{
    public class UsuarioSelectFacade : IUsuarioSelectFacade
    {
        private readonly IUsuarioSelectService _service;

        public UsuarioSelectFacade(IUsuarioSelectService service)
        {
            _service = service;
        }

        public Task<ServiceResult<UsuarioResponse>> Select(UsuarioLoginRequest usuarioRequest)
        {
            return _service.Select(usuarioRequest);
        }

        public Task<ServiceResult<ICollection<UsuarioResponse>>> Select()
        {
            return _service.Select();
        }
    }
}
