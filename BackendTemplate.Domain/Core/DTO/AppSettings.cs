namespace BackendTemplate.Domain.Core.DTO
{
    public class AppSettings : Settings
    {
        private string dbPassword;

        public bool IsLocalhost { get; set; }
        public bool HasStaticFiles { get; set; }

        public string DbPassword
        {
            get
            {
                if (string.IsNullOrWhiteSpace(dbPassword) && Database != null &&
                    !string.IsNullOrWhiteSpace(Database.Password))
                {
                    dbPassword = Database.Password;
                }

                return dbPassword;
            }
            set
            {
                dbPassword = value;
            }
        }

        public string ConnectionString
        {
            get
            {
                if (Database == null)
                {
                    return "DevConnection: Server = (localdb)\\MSSQLLocalDB; Database = TemplateDB; Trusted_Connection = True; MultipleActiveResultSets = True;";
                }

                var connectionStringSQLServer = $"Data Source={Database.Host};Initial Catalog={Database.Catalog};User ID={Database.Username};Password={DbPassword};";
                var connectionStringPostgreSQL = $"Host={Database.Host};Port={Database.Port};Database={Database.Catalog};Username={Database.Username};Password={DbPassword};";

                var connectionString = Database.Type == "SQLServer"
                    ? connectionStringSQLServer
                    : connectionStringPostgreSQL;

                return connectionString;
            }
        }     
    }
}
