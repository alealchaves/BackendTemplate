using BackendTemplate.Domain.Core.DTO;
using BackendTemplate.Domain.DTO.UsuarioDTOs;
using BackendTemplate.Domain.Interfaces.UsuarioInterfaces;
using BackendTemplate.Infra.CrossCode;
using System.Threading.Tasks;

namespace ElevaMobile.Domain.Services.Usuario
{
    public class UsuarioUpdateService : IUsuarioUpdateService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioUpdateService(IUsuarioRepository repository)
        {
            _usuarioRepository = repository;
        }

        public async Task<ServiceResult<bool>> AlterarSenha(AlterarSenhaRequest alterarSenhaRequest)
        {
            var result = new ServiceResult<bool>();

            var senhaAntiga = alterarSenhaRequest.Senha.ToSHA();
            var senhaNova = alterarSenhaRequest.NovaSenha.ToSHA();

            if (!alterarSenhaRequest.NovaSenha.Equals(senhaAntiga))
            {
                result.AddError("Senha anterior incorreta");
                return result;
            }

            var usuario = await _usuarioRepository.SelectByHash(alterarSenhaRequest.Hash);

            usuario.Senha = senhaNova;

            await _usuarioRepository.Update(usuario);

            result.Data = true;

            return result;
        }
    }
}
