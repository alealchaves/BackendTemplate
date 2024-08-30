using BackendTemplate.Domain.Core.i18n;
using BackendTemplate.Domain.DTO.UsuarioDTOs;
using BackendTemplate.Domain.Interfaces.UsuarioInterfaces;
using FluentValidation;

namespace BackendTemplate.Domain.Services.Usuario.Validator
{
    public class UsuarioAlterarSenhaRequestValidator : AbstractValidator<AlterarSenhaRequest>, IUsuarioAlterarSenhaRequestValidator
    {
        public UsuarioAlterarSenhaRequestValidator(IGlobalizationResource localizer)
        {
            RuleFor(u => u.Senha).NotEqual(u => u.NovaSenha)
                .WithMessage(x => localizer["usuarioSenhasDiferentes"]);

        }
    }
}
