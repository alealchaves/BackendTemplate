using BackendTemplate.Domain.Core.DTO;
using BackendTemplate.Domain.Core.Interfaces;
using BackendTemplate.Domain.DTO.UsuarioDTOs;
using BackendTemplate.Domain.Interfaces.UsuarioInterfaces;
using System.Threading.Tasks;

namespace BarramentoPedagogico.Application.Usuario
{
    public class UsuarioUpdateFacade : IUsuarioUpdateFacade
    {
        private readonly IUsuarioUpdateService _service;
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioUpdateFacade(IUnitOfWork unitOfWork, IUsuarioUpdateService service)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<bool>> AlterarSenha(AlterarSenhaRequest alterarSenhaRequest)
        {
            return await _unitOfWork.Execute(() =>
                _service.AlterarSenha(alterarSenhaRequest)
            );
        }
    }
}
