using API_PGD.Databases;
using API_PGD.Models;
using System.Data;
using System.Data.SqlClient;

namespace API_PGD.Repositories
{
    public class IssueTypeRepositorie
    {
        private readonly IConfiguration _configuration;

        public IssueTypeRepositorie(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void DeleteIssueType(Guid id)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "DELETE FROM [IssueType] WHERE ID=@ID";

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

        public List<IssueType> GetAllIssuesTypes()
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            List<IssueType> taskTypes = null;
            IssueType taskType = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "SELECT * FROM [IssueType]";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();

                taskTypes = new List<IssueType>();

                while (sqlDataReader.Read())
                {
                    taskType = new IssueType();
                    taskType.Id = new Guid(Convert.ToString(sqlDataReader["ID"]));
                    taskType.Name = Convert.ToString(sqlDataReader["Name"]);
                    taskType.Description = Convert.ToString(sqlDataReader["Description"]);

                    taskTypes.Add(taskType);
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

            return taskTypes;
        }

        public List<IssueType> GetIssueTypeId(Guid id)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            List<IssueType> taskTypes = null;
            IssueType taskType = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "SELECT * FROM [IssueType] WHERE ID=@ID";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);
                sqlCommand.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = id;

                sqlDataReader = sqlCommand.ExecuteReader();

                taskTypes = new List<IssueType>();

                while (sqlDataReader.Read())
                {
                    taskType = new IssueType();
                    taskType.Id = new Guid(Convert.ToString(sqlDataReader["ID"]));
                    taskType.Name = Convert.ToString(sqlDataReader["Name"]);
                    taskType.Description = Convert.ToString(sqlDataReader["Description"]);

                    taskTypes.Add(taskType);
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

            return taskTypes;
        }

        public object InsertIssueType(IssueType taskType)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            string queryCommand = string.Empty;
            object result = null;

            try
            {
                queryCommand = "INSERT INTO [IssueType] (Name, Description) OUTPUT INSERTED.ID VALUES (@Name,@Description);";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);

                sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = taskType.Name;
                sqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = taskType.Description;

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

        public string UpdateIssueType(IssueType taskType)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "UPDATE [IssueType] SET Name=@Name,Description=@Description WHERE ID=@ID;";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);

                sqlCommand.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = taskType.Id;
                sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = taskType.Name;
                sqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = taskType.Description;

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
