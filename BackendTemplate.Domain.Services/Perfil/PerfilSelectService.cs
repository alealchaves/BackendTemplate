using BackendTemplate.Domain.Core.DTO;
using BackendTemplate.Domain.Core.i18n;
using BackendTemplate.Domain.DTO.PerfilDTOs;
using BackendTemplate.Domain.Interfaces.PerfilInterfaces;
using BackendTemplate.Domain.Interfaces.UsuarioInterfaces;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendTemplate.Domain.Services.Perfil
{
    public class PerfilSelectService : IPerfilSelectService
    {
        private readonly IPerfilRepository _perfilRepository;
        public PerfilSelectService(
            IPerfilRepository perfilRepository,
            IGlobalizationResource localizer)
        {
            _perfilRepository = perfilRepository;
        }

        public async Task<ServiceResult<PerfilResponse>> Select(PerfilRequest perfilRequest)
        {
            var result = new ServiceResult<PerfilResponse>();
            var perfil = await _perfilRepository.SelectById<PerfilResponse>(perfilRequest.Id);

            result.Data = perfil;

            return result;
        }

        public async Task<ServiceResult<ICollection<PerfilResponse>>> Select()
        {
            var result = new ServiceResult<ICollection<PerfilResponse>>();
            var perfis = await _perfilRepository.Select<PerfilResponse>();

            result.Data = perfis;

            return result;
        }
    }
}
