using System;

namespace BackendTemplate.Domain.DTO.UsuarioDTOs
{
    public class AlterarSenhaRequest
    {
        public Guid Hash { get; set; }
        public string Senha { get; set; }
        public string NovaSenha { get; set; }    }
}
