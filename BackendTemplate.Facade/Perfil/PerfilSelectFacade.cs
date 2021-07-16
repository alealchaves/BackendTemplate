using BackendTemplate.Domain.Core.DTO;
using BackendTemplate.Domain.DTO.PerfilDTOs;
using BackendTemplate.Domain.Interfaces.PerfilInterfaces;
using System.Threading.Tasks;

namespace BarramentoPedagogico.Application.Usuario
{
    public class PerfilSelectFacade : IPerfilSelectFacade
    {
        private readonly IPerfilSelectService _service;

        public PerfilSelectFacade(IPerfilSelectService service)
        {
            _service = service;
        }

        public Task<ServiceResult<PerfilResponse>> Select(PerfilRequest perfilRequest)
        {
            return _service.Select(perfilRequest);
        }
    }
}
