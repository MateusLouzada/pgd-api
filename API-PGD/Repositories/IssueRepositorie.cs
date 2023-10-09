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
            List<Models.Issue> issues = null;
            Issue issue = null;

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

                issues = new List<Models.Issue>();

                while (sqlDataReader.Read())
                {
                    issue = new Models.Issue();
                    issue.Id = new Guid(Convert.ToString(sqlDataReader["ID"]));
                    issue.Title = Convert.ToString(sqlDataReader["Title"]);
                    issue.Description = Convert.ToString(sqlDataReader["Description"]);
                    issue.AssignedToUserId = new Guid(Convert.ToString(sqlDataReader["AssignedToUserID"]));
                    issue.CurrentStageId = new Guid(Convert.ToString(sqlDataReader["CurrentStageID"]));
                    issue.IssueTypeId = new Guid(Convert.ToString(sqlDataReader["IssueTypeID"]));
                    issue.ProjectId = new Guid(Convert.ToString(sqlDataReader["ProjectID"]));
                    issue.CreationDate = Convert.ToDateTime(sqlDataReader["CreationDate"]);
                    issue.FinishDate = Convert.ToDateTime(sqlDataReader["FinishDate"]);

                    issues.Add(issue);
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

            return issues;
        }

        public List<Issue> GetIssueId(Guid id)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            List<Issue> issues = null;
            Issue issue = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "SELECT * FROM [Issue] WHERE ID=@ID";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);
                sqlCommand.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = id;

                sqlDataReader = sqlCommand.ExecuteReader();

                issues = new List<Issue>();

                while (sqlDataReader.Read())
                {
                    issue = new Models.Issue();
                    issue.Id = new Guid(Convert.ToString(sqlDataReader["ID"]));
                    issue.Title = Convert.ToString(sqlDataReader["Title"]);
                    issue.Description = Convert.ToString(sqlDataReader["Description"]);
                    issue.AssignedToUserId = new Guid(Convert.ToString(sqlDataReader["AssignedToUserID"]));
                    issue.CurrentStageId = new Guid(Convert.ToString(sqlDataReader["CurrentStageID"]));
                    issue.IssueTypeId = new Guid(Convert.ToString(sqlDataReader["IssueTypeID"]));
                    issue.ProjectId = new Guid(Convert.ToString(sqlDataReader["ProjectID"]));
                    issue.CreationDate = Convert.ToDateTime(sqlDataReader["CreationDate"]);
                    issue.FinishDate = Convert.ToDateTime(sqlDataReader["FinishDate"]);

                    issues.Add(issue);
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

            return issues;
        }

        public object InsertIssue(Issue issue)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            string queryCommand = string.Empty;
            object result = null;

            try
            {
                queryCommand = "INSERT INTO [Issue] (Title, Description, AssignedToUserID, CurrentStageID, IssueTypeID, ProjectID, CreationDate, FinishDate) OUTPUT INSERTED.ID " +
                    "VALUES (@Title,@Description,@AssignedToUserID,@CurrentStageID,@IssueTypeID,@ProjectID,@CreationDate,@FinishDate);";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);

                sqlCommand.Parameters.Add("@Title", SqlDbType.NVarChar).Value = issue.Title;
                sqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = issue.Description;
                sqlCommand.Parameters.Add("@AssignedToUserID", SqlDbType.UniqueIdentifier).Value = issue.AssignedToUserId;
                sqlCommand.Parameters.Add("@CurrentStageID", SqlDbType.UniqueIdentifier).Value = issue.CurrentStageId;
                sqlCommand.Parameters.Add("@IssueTypeID", SqlDbType.UniqueIdentifier).Value = issue.IssueTypeId;
                sqlCommand.Parameters.Add("@ProjectID", SqlDbType.UniqueIdentifier).Value = issue.ProjectId;
                sqlCommand.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = issue.CreationDate;
                sqlCommand.Parameters.Add("@FinishDate", SqlDbType.DateTime).Value = issue.FinishDate;

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

        public string UpdateIssue(Issue issue)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "UPDATE [Issue] SET Title=@Title, Description=@Description, AssignedToUserID=@AssignedToUserID, CurrentStageID=@CurrentStageID, IssueTypeID=@TaskTypeID, ProjectID=@ProjectID, CreationDate=@CreationDate, FinishDate=@FinishDate" +
                    " WHERE ID=@ID;";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);

                sqlCommand.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = issue.Id;
                sqlCommand.Parameters.Add("@Title", SqlDbType.NVarChar).Value = issue.Title;
                sqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = issue.Description;
                sqlCommand.Parameters.Add("@AssignedToUserID", SqlDbType.UniqueIdentifier).Value = issue.AssignedToUserId;
                sqlCommand.Parameters.Add("@CurrentStageID", SqlDbType.UniqueIdentifier).Value = issue.CurrentStageId;
                sqlCommand.Parameters.Add("@IssueTypeID", SqlDbType.UniqueIdentifier).Value = issue.IssueTypeId;
                sqlCommand.Parameters.Add("@ProjectID", SqlDbType.UniqueIdentifier).Value = issue.ProjectId;
                sqlCommand.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = issue.CreationDate;
                sqlCommand.Parameters.Add("@FinishDate", SqlDbType.DateTime).Value = issue.FinishDate;

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
