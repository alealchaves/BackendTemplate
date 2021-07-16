using BackendTemplate.Domain.Core.DTO;
using BackendTemplate.Domain.Core.i18n;
using BackendTemplate.Domain.DTO.UsuarioDTOs;
using BackendTemplate.Domain.Interfaces.UsuarioInterfaces;
using BackendTemplate.Infra.CrossCode;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendTemplate.Domain.Services.Usuario
{
    public class UsuarioSelectService : IUsuarioSelectService
    {
        private readonly IUsuarioLoginRequestValidator _usuarioValidator;
        private readonly IGlobalizationResource _localizer;
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioSelectService(
            IUsuarioRepository usuarioRepository,
            IUsuarioLoginRequestValidator usuarioValidator,
            IGlobalizationResource localizer)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioValidator = usuarioValidator;
            _localizer = localizer;
        }

        public async Task<ServiceResult<UsuarioResponse>> Select(UsuarioLoginRequest usuarioLoginRequest)
        {
            var result = new ServiceResult<UsuarioResponse>();
            ValidationResult validationResult = null;

            validationResult = _usuarioValidator.Validate(usuarioLoginRequest);

            if (!validationResult.IsValid)
            {
                result.AddErrors(validationResult.Errors);
                return result;
            }

            var usuario = await this._usuarioRepository.Select<UsuarioResponse>(usuarioLoginRequest);

            if (usuario == null)
            {
                result.AddError(_localizer["usuarioSenhaIncorretos"]);
                return result;
            }

            result.Data = usuario;
            return result;
        }        

        public async Task<ServiceResult<ICollection<UsuarioResponse>>> Select()
        {
            var result = new ServiceResult<ICollection<UsuarioResponse>>();
            var usuarios = await _usuarioRepository.Select<UsuarioResponse>();
            result.Data = usuarios;

            return result;
        }
    }
}
