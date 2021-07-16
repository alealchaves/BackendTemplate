using AutoMapper;
using BackendTemplate.Domain.DTO.PerfilDTOs;
using BackendTemplate.Domain.DTO.UsuarioDTOs;
using BackendTemplate.Domain.Entities;

namespace BackendTemplate.Api.Core.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UsuarioLoginRequest, Usuario>()
            .ForMember(entity => entity.Email, dto => dto.MapFrom(m => m.Email))
            .ForMember(entity => entity.Senha, dto => dto.MapFrom(m => m.Senha));

            CreateMap<AlterarSenhaRequest, Usuario>()
            .ForMember(entity => entity.Hash, dto => dto.MapFrom(m => m.Hash))
            .ForMember(entity => entity.Senha, dto => dto.MapFrom(m => m.NovaSenha))
            .ForMember(entity => entity.Senha, dto => dto.MapFrom(m => m.Senha));

            CreateMap<PerfilRequest, Perfil>()
            .ForMember(entity => entity.Id, dto => dto.MapFrom(m => m.Id));
        }
    }
}
