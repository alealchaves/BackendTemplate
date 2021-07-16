using BackendTemplate.Domain.Entities;
using System;
using System.Collections.Generic;

namespace BackendTemplate.Domain.DTO.PerfilDTOs
{
    public class PerfilResponse
    {
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public Guid Hash { get; set; }
    }
}
