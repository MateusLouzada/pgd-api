using API_PGD.Databases;
using API_PGD.Models;

using System.Data;
using System.Data.SqlClient;

namespace API_PGD.Repositories
{
    public class CommentRepositorie
    {
        private readonly IConfiguration _configuration;

        public CommentRepositorie(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void DeleteComment(Guid id)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "DELETE FROM [Comment] WHERE ID=@ID";

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

        public List<Comment> GetAllComments()
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            List<Comment> comments = null;
            Comment comment = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "SELECT * FROM [Comment]";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();

                comments = new List<Comment>();
                while (sqlDataReader.Read())
                {
                    comment = new Comment();
                    comment.Id = new Guid(Convert.ToString(sqlDataReader["ID"]));
                    comment.TaskId = new Guid(Convert.ToString(sqlDataReader["TaskID"]));
                    comment.UserId = new Guid(Convert.ToString(sqlDataReader["UserID"]));
                    comment.Content = Convert.ToString(sqlDataReader["Content"]);

                    comments.Add(comment);
                }
            }
            catch (Exception exception) 
            { 
                throw exception; 
            }
            finally {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return comments;
        }

        public List<Comment> GetCommentId(Guid id)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            List<Comment> comments = null;
            Comment comment = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "SELECT * FROM [Comment] WHERE ID=@ID";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);
                sqlCommand.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = id;

                sqlDataReader = sqlCommand.ExecuteReader();

                comments = new List<Comment>();
                while (sqlDataReader.Read())
                {
                    comment = new Comment();
                    comment.Id = new Guid(Convert.ToString(sqlDataReader["ID"]));
                    comment.TaskId = new Guid(Convert.ToString(sqlDataReader["TaskID"]));
                    comment.UserId = new Guid(Convert.ToString(sqlDataReader["UserID"]));
                    comment.Content = Convert.ToString(sqlDataReader["Content"]);

                    comments.Add(comment);
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return comments;
        }

        public Comment InsertComment(Comment comment)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            string queryCommand = string.Empty;
            object result = null;

            try
            {
                queryCommand = "INSERT INTO [Comment] (TaskID, UserID, Content) OUTPUT INSERTED.ID VALUES (@TaskID,@UserID,@Content);";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand(queryCommand, sqlConnection);

                sqlCommand.Parameters.Add("@TaskID", SqlDbType.UniqueIdentifier).Value = comment.TaskId;
                sqlCommand.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = comment.UserId;
                sqlCommand.Parameters.Add("@Content", SqlDbType.NVarChar).Value = comment.Content;

                result = sqlCommand.ExecuteScalar();
                comment.Id = new Guid(Convert.ToString(result));
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

            return comment;
        }

        public string UpdateComment(Comment comment)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            string queryCommand = string.Empty;

            try
            {
                queryCommand = "UPDATE [Comment] SET TaskID=@TaskID, UserID=@UserID, Content=@Content WHERE ID=@ID;";

                sqlConnection = new DB_SGD_SqlServer(_configuration).OpenConnection();
                sqlCommand = new SqlCommand( queryCommand, sqlConnection);

                sqlCommand.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value= comment.Id;
                sqlCommand.Parameters.Add("@TaskID", SqlDbType.UniqueIdentifier).Value = comment.TaskId;
                sqlCommand.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = comment.UserId;
                sqlCommand.Parameters.Add("@Content", SqlDbType.NVarChar).Value = comment.Content;

                sqlCommand.ExecuteNonQuery();
            }
            catch(Exception exception)
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
