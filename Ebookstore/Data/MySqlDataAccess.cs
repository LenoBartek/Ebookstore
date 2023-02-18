namespace Ebookstore.Data
{
    public class MySqlDataAccess
    {
        private readonly IConfiguration _config;

        public MySqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public string GetConnectionString(string connectionName = "MysqlConnection") => _config.GetConnectionString(connectionName);
    }
}
