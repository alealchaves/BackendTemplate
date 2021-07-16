using BackendTemplate.Domain.Core.Interfaces;

namespace BackendTemplate.Domain.Core.DTO
{ 
    public class Settings : ISettings
    {
        public DatabaseSettings Database { get; set; }
    }
}
