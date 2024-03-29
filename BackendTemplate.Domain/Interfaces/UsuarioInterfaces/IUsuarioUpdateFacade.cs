﻿using BackendTemplate.Domain.Core.DTO;
using BackendTemplate.Domain.Core.Interfaces;
using BackendTemplate.Domain.DTO.UsuarioDTOs;
using System.Threading.Tasks;

namespace BackendTemplate.Domain.Interfaces.UsuarioInterfaces
{
    public interface IUsuarioUpdateFacade : IBaseFacade
    {
        Task<ServiceResult<bool>> AlterarSenha(AlterarSenhaRequest alterarSenhaRequest);
    }
}
