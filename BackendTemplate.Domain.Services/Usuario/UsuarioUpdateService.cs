using BackendTemplate.Domain.Core.DTO;
using BackendTemplate.Domain.Core.i18n;
using BackendTemplate.Domain.DTO.UsuarioDTOs;
using BackendTemplate.Domain.Interfaces.UsuarioInterfaces;
using BackendTemplate.Infra.CrossCode;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace ElevaMobile.Domain.Services.Usuario
{
    public class UsuarioUpdateService : IUsuarioUpdateService
    {
        private readonly IUsuarioAlterarSenhaRequestValidator _alterarSenhaValidator;
        private readonly IGlobalizationResource _localizer;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioUpdateService(IUsuarioRepository usuarioRepository,
            IUsuarioAlterarSenhaRequestValidator alterarSenhaValidator,
            IGlobalizationResource localizer)
        {
            _usuarioRepository = usuarioRepository;
            _alterarSenhaValidator = alterarSenhaValidator;
            _localizer = localizer;
        }

        public async Task<ServiceResult<bool>> AlterarSenha(AlterarSenhaRequest alterarSenhaRequest)
        {
            var result = new ServiceResult<bool>();
            ValidationResult validationResult = null;

            validationResult = _alterarSenhaValidator.Validate(alterarSenhaRequest);

            if (!validationResult.IsValid)
            {
                result.AddErrors(validationResult.Errors);
                return result;
            }

            var usuario = await _usuarioRepository.SelectByHash(alterarSenhaRequest.Hash);
            usuario.Senha = alterarSenhaRequest.NovaSenha;

            await _usuarioRepository.Update(usuario);

            result.Data = true;

            return result;
        }
    }
}
