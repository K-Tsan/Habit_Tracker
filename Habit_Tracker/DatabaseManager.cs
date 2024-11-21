using Microsoft.Data.Sqlite;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Habit_Tracker.Program;

namespace Habit_Tracker
{
    internal class DatabaseManager
    {
        private string connectionString;
        public DatabaseManager(string path)
        {
            connectionString = $"Data Source={path}";
            InitialiseDatabase(connectionString);
        }

        public static void InitialiseDatabase(string connectionString)
        {
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
        public void Insert(string date, int quantity)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCommand = connection.CreateCommand();

                tableCommand.CommandText = 
                    $"INSERT INTO habit_table(date, quantity) VALUES('{date}', {quantity})";

                tableCommand.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
