using API_PGD.Databases;
using API_PGD.Models;
using System.Data;
using System.Data.SqlClient;

namespace API_PGD.Repositories
{
    public class IssueRepositorie
    {
        private readonly IConfiguration _configuration;

        public IssueRepositorie(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void DeleteIssue(Guid id)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "DELETE FROM [Issue] WHERE ID=@ID";

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

        public List<Models.Issue> GetAllIssues(string? stageId)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            List<Models.Issue> tasks = null;
            Models.Issue task = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "SELECT * FROM [Issue] WHERE 1=1 ";


                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);
                if (!string.IsNullOrEmpty(stageId))
                {
                    sqlCommand.CommandText += " AND CurrentStageID=@stageID";
                    sqlCommand.Parameters.Add("@stageID", SqlDbType.UniqueIdentifier).Value = new Guid(stageId);
                }
                sqlDataReader = sqlCommand.ExecuteReader();

                tasks = new List<Models.Issue>();

                while (sqlDataReader.Read())
                {
                    task = new Models.Issue();
                    task.Id = new Guid(Convert.ToString(sqlDataReader["ID"]));
                    task.Title = Convert.ToString(sqlDataReader["Title"]);
                    task.Description = Convert.ToString(sqlDataReader["Description"]);
                    task.AssignedToUserId = new Guid(Convert.ToString(sqlDataReader["AssignedToUserID"]));
                    task.CurrentStageId = new Guid(Convert.ToString(sqlDataReader["CurrentStageID"]));
                    task.TaskTypeId = new Guid(Convert.ToString(sqlDataReader["TaskTypeID"]));
                    task.ProjectId = new Guid(Convert.ToString(sqlDataReader["ProjectID"]));
                    task.CreationDate = Convert.ToDateTime(sqlDataReader["CreationDate"]);
                    task.FinishDate = Convert.ToDateTime(sqlDataReader["FinishDate"]);

                    tasks.Add(task);
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

            return tasks;
        }

        public List<Issue> GetIssueId(Guid id)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            List<Models.Issue> tasks = null;
            Models.Issue task = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "SELECT * FROM [Issue] WHERE ID=@ID";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);
                sqlCommand.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = id;

                sqlDataReader = sqlCommand.ExecuteReader();

                tasks = new List<Models.Issue>();

                while (sqlDataReader.Read())
                {
                    task = new Models.Issue();
                    task.Id = new Guid(Convert.ToString(sqlDataReader["ID"]));
                    task.Title = Convert.ToString(sqlDataReader["Title"]);
                    task.Description = Convert.ToString(sqlDataReader["Description"]);
                    task.AssignedToUserId = new Guid(Convert.ToString(sqlDataReader["AssignedToUserID"]));
                    task.CurrentStageId = new Guid(Convert.ToString(sqlDataReader["CurrentStageID"]));
                    task.TaskTypeId = new Guid(Convert.ToString(sqlDataReader["TaskTypeID"]));
                    task.ProjectId = new Guid(Convert.ToString(sqlDataReader["ProjectID"]));
                    task.CreationDate = Convert.ToDateTime(sqlDataReader["CreationDate"]);
                    task.FinishDate = Convert.ToDateTime(sqlDataReader["FinishDate"]);

                    tasks.Add(task);
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

            return tasks;
        }

        public object InsertIssue(Issue task)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            string queryCommand = string.Empty;
            object result = null;

            try
            {
                queryCommand = "INSERT INTO [Issue] (Title, Description, AssignedToUserID, CurrentStageID, TaskTypeID, ProjectID, CreationDate, FinishDate) OUTPUT INSERTED.ID " +
                    "VALUES (@Title,@Description,@AssignedToUserID,@CurrentStageID,@TaskTypeID,@ProjectID,@CreationDate,@FinishDate);";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);

                sqlCommand.Parameters.Add("@Title", SqlDbType.NVarChar).Value = task.Title;
                sqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = task.Description;
                sqlCommand.Parameters.Add("@AssignedToUserID", SqlDbType.UniqueIdentifier).Value = task.AssignedToUserId;
                sqlCommand.Parameters.Add("@CurrentStageID", SqlDbType.UniqueIdentifier).Value = task.CurrentStageId;
                sqlCommand.Parameters.Add("@TaskTypeID", SqlDbType.UniqueIdentifier).Value = task.TaskTypeId;
                sqlCommand.Parameters.Add("@ProjectID", SqlDbType.UniqueIdentifier).Value = task.ProjectId;
                sqlCommand.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = task.CreationDate;
                sqlCommand.Parameters.Add("@FinishDate", SqlDbType.DateTime).Value = task.FinishDate;

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

        public string UpdateIssue(Issue task)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "UPDATE [Issue] SET Title=@Title, Description=@Description, AssignedToUserID=@AssignedToUserID, CurrentStageID=@CurrentStageID, TaskTypeID=@TaskTypeID, ProjectID=@ProjectID, CreationDate=@CreationDate, FinishDate=@FinishDate" +
                    " WHERE ID=@ID;";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);

                sqlCommand.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = task.Id;
                sqlCommand.Parameters.Add("@Title", SqlDbType.NVarChar).Value = task.Title;
                sqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = task.Description;
                sqlCommand.Parameters.Add("@AssignedToUserID", SqlDbType.UniqueIdentifier).Value = task.AssignedToUserId;
                sqlCommand.Parameters.Add("@CurrentStageID", SqlDbType.UniqueIdentifier).Value = task.CurrentStageId;
                sqlCommand.Parameters.Add("@TaskTypeID", SqlDbType.UniqueIdentifier).Value = task.TaskTypeId;
                sqlCommand.Parameters.Add("@ProjectID", SqlDbType.UniqueIdentifier).Value = task.ProjectId;
                sqlCommand.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = task.CreationDate;
                sqlCommand.Parameters.Add("@FinishDate", SqlDbType.DateTime).Value = task.FinishDate;

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
