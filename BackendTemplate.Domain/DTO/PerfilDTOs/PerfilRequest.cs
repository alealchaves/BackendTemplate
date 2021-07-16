using System;

namespace BackendTemplate.Domain.DTO.PerfilDTOs
{
    public class PerfilRequest
    {
        public PerfilRequest()
        {
        }

        public PerfilRequest(int id) : base()
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
