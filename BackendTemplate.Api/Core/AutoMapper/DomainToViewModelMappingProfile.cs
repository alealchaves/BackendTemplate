using AutoMapper;
using BackendTemplate.Domain.DTO.PerfilDTOs;
using BackendTemplate.Domain.DTO.UsuarioDTOs;
using BackendTemplate.Domain.Entities;

namespace BackendTemplate.Api.Core.AutoMapper
{
    /// <summary>
    /// DomainToViewModelMappingProfile para mapeamento de classes de dominio
    /// </summary>
    public class DomainToViewModelMappingProfile : Profile
    {
        /// <summary>
        /// Constructor do DomainToViewModelMappingProfile
        /// </summary>
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Usuario, UsuarioResponse>()
            .ForMember(dto => dto.Hash, entity => entity.MapFrom(m => m.Hash))
            .ForMember(dto => dto.Nome, entity => entity.MapFrom(m => m.Nome))
            .ForMember(dto => dto.Email, entity => entity.MapFrom(m => m.Email))
            .ForMember(dto => dto.Ativo, entity => entity.MapFrom(m => m.EntityControl.Ativo))
            .ForMember(dto => dto.DataInclusao, entity => entity.MapFrom(m => m.EntityControl.DataInclusao))
            .ForMember(dto => dto.UsuarioPerfis, entity => entity.MapFrom(m => m.UsuarioPerfis));

            CreateMap<Perfil, PerfilResponse>()
           .ForMember(dto => dto.Hash, entity => entity.MapFrom(m => m.Hash))
           .ForMember(dto => dto.Nome, entity => entity.MapFrom(m => m.Nome))
           .ForMember(dto => dto.Ativo, entity => entity.MapFrom(m => m.EntityControl.Ativo));
        }
    }
}