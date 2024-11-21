using System;
using Microsoft.Data.Sqlite;

namespace Habit_Tracker
{
    class Program
    {
        public enum Operations{ Create = 1, Read = 2, Update = 3, Delete = 4}
        static void Main(string[] args)
        {
            var databaseManager = new DatabaseManager("habit-tracker.db");
            var menu = new Menu();
            var input = menu.RunMenu();
            Operations operation = (Operations)input;
        }
    }
}

