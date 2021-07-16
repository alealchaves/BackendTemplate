using AutoMapper;
using BackendTemplate.Domain.Entities;
using BackendTemplate.Domain.Interfaces.PerfilInterfaces;
using BackendTemplate.Infra.Data.Core.Repositories;
using Microsoft.AspNetCore.Http;

namespace BackendTemplate.Infra.Data.Repositories
{
    public class PerfilRepository : GeneralRepository<Perfil>, IPerfilRepository
    {
        public PerfilRepository(MyAppContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
        }
       
    }
}