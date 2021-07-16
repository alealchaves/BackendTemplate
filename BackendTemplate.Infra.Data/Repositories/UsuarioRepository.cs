using AutoMapper;
using BackendTemplate.Domain.DTO.UsuarioDTOs;
using BackendTemplate.Domain.Entities;
using BackendTemplate.Domain.Interfaces.UsuarioInterfaces;
using BackendTemplate.Infra.CrossCode;
using BackendTemplate.Infra.Data.Core.Repositories;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BackendTemplate.Infra.Data.Repositories
{
    public class UsuarioRepository : GeneralRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(MyAppContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
        }
        public async Task<TDTO> Select<TDTO>(UsuarioLoginRequest usuarioRequest)
        {
            TDTO usuarioResponse = default(TDTO);

            if (usuarioRequest != null)
            {
                if (!string.IsNullOrWhiteSpace(usuarioRequest.Senha))
                {
                    if (!string.IsNullOrWhiteSpace(usuarioRequest.Cpf))
                        usuarioResponse = await SelectFirst<TDTO>(a => a.Cpf.Equals(usuarioRequest.Cpf)
                        && a.Senha.Equals(usuarioRequest.Senha.ToSHA()));

                    if (usuarioResponse == null && !string.IsNullOrWhiteSpace(usuarioRequest.Email))
                        usuarioResponse = await SelectFirst<TDTO>(a => a.Email.Equals(usuarioRequest.Email.Trim())
                        && a.Senha.Equals(usuarioRequest.Senha.ToSHA()));
                }
            }

            return usuarioResponse;
        }       
    }
}