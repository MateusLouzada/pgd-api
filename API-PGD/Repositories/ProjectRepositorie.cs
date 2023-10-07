using API_PGD.Databases;
using API_PGD.Models;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace API_PGD.Repositories
{
    public class ProjectRepositorie
    {
        private readonly IConfiguration _configuration;

        public ProjectRepositorie(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void DeleteProject(Guid id)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "DELETE FROM [Project] WHERE ID=@ID";

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

        public List<Project> GetAllProjects()
        {

            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            List<Project> projects = null;
            Project project = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "SELECT * FROM [Project]";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();

                projects = new List<Project>();

                while (sqlDataReader.Read())
                {
                    project = new Project();
                    project.Id = new Guid(Convert.ToString(sqlDataReader["ID"]));
                    project.Name = Convert.ToString(sqlDataReader["Name"]);
                    project.Description = Convert.ToString(sqlDataReader["Description"]);
                    project.MainUserID = new Guid(Convert.ToString(sqlDataReader["MainUserID"]));

                    projects.Add(project);
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

            return projects;
        }

        public List<Project> GetProjectId(Guid id)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            List<Project> projects = null;
            Project project = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "SELECT * FROM [Project] WHERE ID=@ID";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);
                sqlCommand.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = id;

                sqlDataReader = sqlCommand.ExecuteReader();

                projects = new List<Project>();
                while (sqlDataReader.Read())
                {
                    project = new Project();
                    project.Id = new Guid(Convert.ToString(sqlDataReader["ID"]));
                    project.Name = Convert.ToString(sqlDataReader["Name"]);
                    project.Description = Convert.ToString(sqlDataReader["Description"]);
                    project.MainUserID = new Guid(Convert.ToString(sqlDataReader["MainUserID"]));

                    projects.Add(project);
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

            return projects;
        }

        public object InsertProject(Project project)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            string queryCommand = string.Empty;
            object result = null;

            try
            {
                queryCommand = "INSERT INTO [Project] (Name, Description, MainUserID) OUTPUT INSERTED.ID VALUES (@Name,@Description,@MainUserID);";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);

                sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = project.Name;
                sqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = project.Description;
                sqlCommand.Parameters.Add("@MainUserID", SqlDbType.NVarChar).Value = project.MainUserID;

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

        public string UpdateProject(Project project)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "UPDATE [Project] SET Name=@Name, Description=@Description, MainUserID=@MainUserID WHERE ID=@ID;";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);

                sqlCommand.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = project.Id;
                sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = project.Name;
                sqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = project.Description;
                sqlCommand.Parameters.Add("@MainUserID", SqlDbType.NVarChar).Value = project.MainUserID;

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
