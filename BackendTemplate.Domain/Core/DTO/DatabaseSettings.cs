namespace BackendTemplate.Domain.Core.DTO
{
    public class DatabaseSettings
    {
        public string Type { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Catalog { get; set; }
        public string Username { get; set; }
        public bool Pooling { get; set; }
        public int MinPoolSize { get; set; }
        public int MaximumPoolSize { get; set; }
        public int Timeout { get; set; }
        public int CommandTimeout { get; set; }
        public string Password { get; set; }
    }
}
