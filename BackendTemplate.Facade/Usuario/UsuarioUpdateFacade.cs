using BackendTemplate.Domain.Core.DTO;
using BackendTemplate.Domain.DTO.UsuarioDTOs;
using BackendTemplate.Domain.Interfaces.UsuarioInterfaces;
using System.Threading.Tasks;

namespace BarramentoPedagogico.Application.Usuario
{
    public class UsuarioUpdateFacade : IUsuarioUpdateFacade
    {
        private readonly IUsuarioUpdateService _service;

        public UsuarioUpdateFacade(IUsuarioUpdateService service)
        {
            _service = service;
        }       

        public async Task<ServiceResult<bool>> AlterarSenha(AlterarSenhaRequest alterarSenhaRequest)
        {
            return await _service.AlterarSenha(alterarSenhaRequest);
        }
    }
}
