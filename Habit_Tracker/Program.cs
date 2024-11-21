using System;
using Microsoft.Data.Sqlite;

namespace Habit_Tracker
{
    class Program
    {
        public enum Operation{ Create = 1, Read = 2, Update = 3, Delete = 4}
        static void Main(string[] args)
        {
            var databaseManager = new DatabaseManager("habit-tracker.db");
            var menu = new Menu();
            do
            {
                var input = menu.RunMenu();
                if (input == 5)
                {
                    Console.WriteLine("Exiting program, press any key.");
                    Console.ReadKey();
                    break;
                }
                Operation operation = (Operation)input;
                switch (operation)
                {
                    case (Operation.Create):
                        {
                            var date = menu.GetDate();
                            var quantity = menu.GetQuantity();
                            databaseManager.Insert(date, quantity);
                            break;
                        }
                    case (Operation.Read):
                        {
                            databaseManager.ViewRecords();
                            break;
                        }
                    case (Operation.Update):
                        {
                            var id = menu.GetId();
                            var date = menu.GetDate();
                            var quantity = menu.GetQuantity();
                            databaseManager.Update(id, date, quantity);
                            break;
                        }
                    case (Operation.Delete):
                        break;
                }
            } while (true);
        }
    }
}

