using BackendTemplate.Domain.Core.Interfaces;
using BackendTemplate.Domain.DTO.UsuarioDTOs;
using FluentValidation.Results;

namespace BackendTemplate.Domain.Interfaces.UsuarioInterfaces
{ 
    public interface IUsuarioAlterarSenhaRequestValidator : IBaseValidator
    {
        ValidationResult Validate(AlterarSenhaRequest request);
    }
}
