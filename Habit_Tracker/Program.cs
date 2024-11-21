using System;
using Microsoft.Data.Sqlite;

namespace Habit_Tracker
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=habitTracker.db";
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCommand = connection.CreateCommand();

                tableCommand.CommandText = @"
                    CREATE TABLE IF NOT EXISTS drinking_water (
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

