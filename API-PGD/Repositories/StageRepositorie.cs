using API_PGD.Databases;
using API_PGD.Models;
using System.Data;
using System.Data.SqlClient;

namespace API_PGD.Repositories
{
    public class StageRepositorie
    {
        private readonly IConfiguration _configuration;

        public StageRepositorie(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void DeleteStage(Guid id)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "DELETE FROM [Stage] WHERE ID=@ID";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);

                sqlCommand.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = id;

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

        public List<Stage> GetAllStages()
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            List<Stage> stages = null;
            Stage stage = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "SELECT * FROM [Stage]";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();

                stages = new List<Stage>();

                while (sqlDataReader.Read())
                {
                    stage = new Stage();
                    stage.Id = new Guid(Convert.ToString(sqlDataReader["ID"]));
                    stage.Name = Convert.ToString(sqlDataReader["Name"]);
                    stage.Description = Convert.ToString(sqlDataReader["Description"]);

                    stages.Add(stage);
                }
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

            return stages;
        }

        public List<Stage> GetStageId(Guid id)
        {

            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            List<Stage> stages = null;
            Stage stage = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "SELECT * FROM [Stage] WHERE ID=@ID";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);
                sqlCommand.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = id;

                sqlDataReader = sqlCommand.ExecuteReader();

                stages = new List<Stage>();

                while (sqlDataReader.Read())
                {
                    stage = new Stage();
                    stage.Id = new Guid(Convert.ToString(sqlDataReader["ID"]));
                    stage.Name = Convert.ToString(sqlDataReader["Name"]);
                    stage.Description = Convert.ToString(sqlDataReader["Description"]);

                    stages.Add(stage);
                }
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

            return stages;
        }

        public object InsertStage(Stage stage)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            string queryCommand = string.Empty;
            object result = null;

            try
            {
                queryCommand = "INSERT INTO [Stage] (Name, Description) OUTPUT INSERTED.ID VALUES (@Name,@Description);";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);

                sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = stage.Name;
                sqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = stage.Description;

                result = sqlCommand.ExecuteScalar();
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

            return result;
        }

        public string UpdateStage(Stage stage)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "UPDATE [Stage] SET Name=@Name, Description=@Description WHERE ID=@ID;";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);

                sqlCommand.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = stage.Id;
                sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = stage.Name;
                sqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = stage.Description;

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

            return "Success";
        }
    }
}
