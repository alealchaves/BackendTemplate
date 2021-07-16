namespace BackendTemplate.Domain.DTO.UsuarioDTOs
{
    public class UsuarioLoginRequest
    {
        public UsuarioLoginRequest()
        {
        }

        public UsuarioLoginRequest(string cpf, string email) : base()
        {
            Cpf = cpf;
            Email = email;
        }

        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
