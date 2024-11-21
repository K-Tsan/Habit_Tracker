using Microsoft.Data.Sqlite;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Habit_Tracker.Program;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public void ViewRecords()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCommand = connection.CreateCommand();
                tableCommand.CommandText =
                    $"SELECT * FROM habit_table";

                List<Habit> tableData = new List<Habit>();
                SqliteDataReader reader = tableCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(new Habit
                        {
                            Id = reader.GetInt32(0),
                            Date = DateTime.ParseExact(reader.GetString(1), "dd-MM-yy", new CultureInfo("en-US")),
                            Quantity = reader.GetInt32(2)
                        });
                    }
                } else
                {
                    Console.WriteLine("Table is empty.");
                    return;
                }
                connection.Close();

                Console.Clear();
                foreach (var habit in tableData)
                {
                    Console.WriteLine($"| {habit.Id} | {habit.Date.ToString("dd-MMM-yyyy")} | Quantity: {habit.Quantity} |");
                }

                Console.WriteLine("\nPress any key to return.");
                Console.ReadKey();
            }
        }

        public void Update(int id, string date, int quantity)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var checkId = connection.CreateCommand();
                checkId.CommandText = $"SELECT EXISTS(SELECT 1 FROM habit_table WHERE Id = {id})";
                int checkQuery = Convert.ToInt32(checkId.ExecuteScalar());

                if (checkQuery == 0)
                {
                    connection.Close();
                    Console.WriteLine($"Record with Id: {id} doesn't exist, press any key to return.");
                    Console.ReadKey();
                    return;
                }

                var tableCommand = connection.CreateCommand();

                tableCommand.CommandText =
                    $"UPDATE habit_table SET Date = '{date}', Quantity = {quantity} WHERE Id = {id}";

                tableCommand.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
