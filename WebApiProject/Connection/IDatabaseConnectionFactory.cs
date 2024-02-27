using Microsoft.Data.SqlClient;
using System.Data;

namespace WebApiProject.Connection
{
    public interface IDatabaseConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
