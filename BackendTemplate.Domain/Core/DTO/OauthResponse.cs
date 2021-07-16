using BackendTemplate.Domain.DTO.UsuarioDTOs;

namespace BackendTemplate.Domain.Core.DTO
{
    public class OauthResponse
    { 
        public string Token { get; set; }
        public UsuarioResponse Usuario { get; set; }
    }
}
