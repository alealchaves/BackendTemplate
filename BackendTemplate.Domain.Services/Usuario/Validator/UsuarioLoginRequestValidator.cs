using BackendTemplate.Domain.Core.i18n;
using BackendTemplate.Domain.DTO.UsuarioDTOs;
using BackendTemplate.Domain.Interfaces.UsuarioInterfaces;
using FluentValidation;

namespace BackendTemplate.Domain.Services.Usuario.Validator
{
    public class UsuarioLoginRequestValidator : AbstractValidator<UsuarioLoginRequest>, IUsuarioLoginRequestValidator
    {
        public UsuarioLoginRequestValidator(IGlobalizationResource localizer)
        {
            RuleFor(u => u.Senha).NotEmpty()
                .WithMessage(x => localizer["usuarioSenhaIncorretos"]);

            RuleFor(u => u.Email).NotEmpty()
                .WithMessage(x => localizer["usuarioSenhaIncorretos"]);
        }
    }
}
