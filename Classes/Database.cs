using System.Data;
using MySql.Data.MySqlClient;

namespace Scheduler.Classes
{
    public class Database
    {
        // ---------- Variables ----------
        public static string Server { get; set; }
        public static string Port { get; set; }
        public static string DbName { get; set; }
        public static string Uid { get; set; }
        public static string Password { get; set; }
        public static bool IsConnected { get; set; }
        public static string Query { get; set; }
        public static DataTable dt { get; set; }



        // ---------- Constructor ----------
        public Database()
        {
            IsConnected = false;
        }



        // ---------- Functions ----------
        public static MySqlConnection Connect()
        {
            string connStr = string.Format("Server={0}; Port={1}; Database={2}; Uid={3}; Password={4}", Server, Port, DbName, Uid, Password);
            MySqlConnection connection = new MySqlConnection(connStr);

            try
            {
                connection.Open();
                IsConnected = true;
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Cannot connect to MySql database", "ERROR");
                IsConnected = false;
            }
            return connection;
        }
    }
}
