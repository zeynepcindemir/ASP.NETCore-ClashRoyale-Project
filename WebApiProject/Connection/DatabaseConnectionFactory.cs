using Microsoft.Data.SqlClient;
using System.Data;

namespace WebApiProject.Connection
{
    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly  IConfiguration _configuration;
        private readonly string _connectionString;
        

        public DatabaseConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SQLConnection");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }


    }

}
