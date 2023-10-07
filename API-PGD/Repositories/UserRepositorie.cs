using API_PGD.Databases;
using API_PGD.Models;
using System.Data;
using System.Data.SqlClient;

namespace API_PGD.Repositories
{
    public class UserRepositorie
    {
        private readonly IConfiguration _configuration;

        public UserRepositorie(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void DeleteUser(Guid id)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "DELETE FROM [User] WHERE ID=@ID";

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

        public List<User> GetAllUsers()
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            List<User> users = null;
            User user = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "SELECT * FROM [User]";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();

                users = new List<User>();

                while (sqlDataReader.Read())
                {
                    user = new User();
                    user.Id = new Guid(Convert.ToString(sqlDataReader["ID"]));
                    user.Name = Convert.ToString(sqlDataReader["Name"]);
                    user.Email = Convert.ToString(sqlDataReader["Email"]);
                    user.Password = Convert.ToString(sqlDataReader["Password"]);

                    users.Add(user);
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

            return users;
        }

        public List<User> GetUserId(Guid id)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            List<User> users = null;
            User user = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "SELECT * FROM [User] WHERE ID=@ID";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);
                sqlCommand.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = id;

                sqlDataReader = sqlCommand.ExecuteReader();

                users = new List<User>();

                while (sqlDataReader.Read())
                {
                    user = new User();
                    user.Id = new Guid(Convert.ToString(sqlDataReader["ID"]));
                    user.Name = Convert.ToString(sqlDataReader["Name"]);
                    user.Email = Convert.ToString(sqlDataReader["Email"]);
                    user.Password = Convert.ToString(sqlDataReader["Password"]);

                    users.Add(user);
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

            return users;
        }

        public object InsertUser(User user)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            string queryCommand = string.Empty;
            object result = null;

            try
            {
                queryCommand = "INSERT INTO [User] (Name, Email, Password) OUTPUT INSERTED.ID VALUES (@Name,@Email,@Password);";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);

                sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = user.Name;
                sqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;
                sqlCommand.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password;

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

        public string UpdateUser(User user)
        {

            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "UPDATE [User] SET Name=@Name,Email=@Email,Password=@Password WHERE ID=@ID;";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);

                sqlCommand.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = user.Id;
                sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = user.Name;
                sqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;
                sqlCommand.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password;

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
