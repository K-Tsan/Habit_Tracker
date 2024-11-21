using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habit_Tracker
{
    internal class DatabaseManager
    {
        private string connectionString;
        public DatabaseManager(string path)
        {
            connectionString = $"Data Source={path}";
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCommand = connection.CreateCommand();

                tableCommand.CommandText = @"
                    CREATE TABLE IF NOT EXISTS habit_table (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Date TEXT,
                        Quantity INTEGER
                        )";

                tableCommand.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
