using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Npgsql;

namespace meetings_and_events.Controllers
{
    public class DatabaseLinkController
    {
        private static DatabaseLinkController _instance;
        private static NpgsqlConnection connection;
        
        private DatabaseLinkController()
        {
        }

        public static DatabaseLinkController GetInstance()
        {
            if (_instance == null)
                _instance = new DatabaseLinkController();
            return _instance;
        }

        private static void Connect()
        {
            string connectionString = DatabaseConfig.database();
            connection = new NpgsqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public NpgsqlConnection GetConnection()
        {
            if (connection == null)
                Connect();
            return connection;
        }

        private bool InitializeDatabase()
        {
            bool complete = false;
            
            if (connection == null)
                Connect();
            
            string sql = "SELECT version();";
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            string version = cmd.ExecuteScalar().ToString();
            
            return complete;
        }
    }
}
