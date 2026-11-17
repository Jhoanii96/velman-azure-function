using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

public class SqlExecutor
{
    private readonly string sqlServerName;
    private readonly string sqlDatabase;
    private readonly string sqlLogin;
    private readonly string sqlPassword;

    public SqlExecutor(string serverName, string database, string login, string password)
    {
        sqlServerName = serverName;
        sqlDatabase = database;
        sqlLogin = login;
        sqlPassword = password;
    }

    public void ExecuteMergeCommand(string full_query)
    {
        string connectionString = $"Data Source={sqlServerName};Initial Catalog={sqlDatabase};User ID={sqlLogin};Password={sqlPassword}";

        using (SqlConnection cnn = new SqlConnection(connectionString))
        {
            Thread.Sleep(3000);  // Delay before attempting connection (can be removed or adjusted)
            try
            {
                // Ensure connection is closed before opening
                if (cnn.State == ConnectionState.Closed)
                {
                    Thread.Sleep(500);  // Additional delay for simulation purposes
                    cnn.Open();
                    Thread.Sleep(500);  // Small delay after connection
                }
                using (SqlCommand command = new SqlCommand(full_query, cnn))
                {
                    command.CommandTimeout = 3600;
                    SqlDataReader reader = command.ExecuteReader();
                    int numRowsAffected = reader.RecordsAffected;
                    reader.Close();
                }
                cnn.Close();
                Thread.Sleep(10);  // Small delay before logging completion
            }
            catch (SqlException sqlEx)
            {
                throw new ArgumentException("An error occurred while executing the SQL command.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("A general error occurred.", ex);
            }
        }
    }
}

