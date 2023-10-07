using System.Data.SqlClient;

namespace API_PGD.Helpers
{
    public class DeleteHelper
    {

        private readonly IConfiguration _configuration;

        public DeleteHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void DeleteHelp(string id, string table)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            string connectionString = string.Empty;
            string queryCommand = string.Empty;

            try
            {
                connectionString = _configuration.GetConnectionString("DefaultConnection");
                queryCommand = "DELETE FROM ["+ table +"] WHERE ID='" + id + "'";

                sqlConnection = new SqlConnection(connectionString);
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
    }
}
