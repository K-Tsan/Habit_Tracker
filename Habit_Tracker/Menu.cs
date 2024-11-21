using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habit_Tracker
{
    internal class Menu
    {
        public int RunMenu()
        {
            Console.Clear();
            do
            {
                Console.WriteLine("Select an action:\n" +
                    "1. Create Record\n" +
                    "2. View All Records\n" +
                    "3. Update Existing Record\n" +
                    "4. Delete Existing Record\n" +
                    "5. Exit\n");
                var userInput = Console.ReadLine();
                int value;
                if (int.TryParse(userInput, out value))
                {
                    if (value > 0 && value <= 5)
                    {
                        return value;
                    }
                }

                Console.Clear();
                Console.WriteLine("Please enter a valid option.");
            } while (true);
        }
    }
}
