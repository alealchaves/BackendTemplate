using BackendTemplate.Domain.Entities;
using System;
using System.Collections.Generic;

namespace BackendTemplate.Domain.DTO.UsuarioDTOs
{
    public class UsuarioResponse
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public Guid Hash { get; set; }
        public DateTime DataInclusao { get; set; }
        public ICollection<UsuarioPerfil> UsuarioPerfis { get; set; }
    }
}
