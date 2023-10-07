using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace API_PGD.Databases
{
    public class DB_SGD_SqlServer
    {
        public string ConnectionString { get; set; }

        private readonly IConfiguration _configuration;

        public DB_SGD_SqlServer(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection OpenConnection()
        {
            SqlConnection sqlConnection = null;

            try
            {              
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();
            }
            catch (Exception execption)
            {
                throw execption;
            }

            return sqlConnection;
        }
    }
}
