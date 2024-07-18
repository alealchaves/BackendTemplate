using AutoMapper;
using BackendTemplate.Domain.DTO.PerfilDTOs;
using BackendTemplate.Domain.DTO.UsuarioDTOs;
using BackendTemplate.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

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
            .ForMember(dto => dto.UsuarioPerfis, entity => entity.MapFrom(m => m.UsuarioPerfis.Select(p => new KeyValuePair<int, string>(p.PerfilId, p.Perfil.Nome))))
            .ForMember(dto => dto.Perfil, entity => entity.MapFrom(m => string.Join("," , m.UsuarioPerfis.Select(p => p.Perfil.Nome))));

            CreateMap<Perfil, PerfilResponse>()
           .ForMember(dto => dto.Id, entity => entity.MapFrom(m => m.Id))
           .ForMember(dto => dto.Nome, entity => entity.MapFrom(m => m.Nome));
        }
    }
}