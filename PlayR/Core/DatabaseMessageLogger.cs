using System;
using System.Data.SqlClient;

namespace PlayR.Core
{
    public class DatabaseMessageLogger : IMessageLogger, IDisposable
    {
        SqlConnection connection;

        public DatabaseMessageLogger()
        {
            connection = GetConnection();
        }

        public void LogMessage(string message, string user)
        {
            using(var command = new SqlCommand("insert into Messages values (@user, @message)", connection))
            {
                command.Parameters.AddWithValue("user", user);
                command.Parameters.AddWithValue("message", message);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        SqlConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }

        string GetConnectionString()
        {
            System.Configuration.Configuration rootWebConfig =
                System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/");
            System.Configuration.ConnectionStringSettings connString;
            if(rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
            {
                connString =
                    rootWebConfig.ConnectionStrings.ConnectionStrings["playr"];
                if(connString != null)
                    return connString.ConnectionString;
            }
            return null;
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}