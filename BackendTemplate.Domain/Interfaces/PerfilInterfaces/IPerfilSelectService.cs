using BackendTemplate.Domain.Core.DTO;
using BackendTemplate.Domain.Core.Interfaces;
using BackendTemplate.Domain.DTO.PerfilDTOs;
using System.Threading.Tasks;

namespace BackendTemplate.Domain.Interfaces.PerfilInterfaces
{
    public interface IPerfilSelectService : IBaseService
    {
        Task<ServiceResult<PerfilResponse>> Select(PerfilRequest perfilRequest);
    }
}