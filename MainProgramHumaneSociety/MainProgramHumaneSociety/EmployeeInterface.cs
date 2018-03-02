using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgramHumaneSociety
{
    class EmployeeInterface
    {
        public EmployeeInterface()
        {

        }

        public void ShowMenuSelection()
        {
            Console.WriteLine("[ Welcome to the Employee Menu ]\n");
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("[1] Add New Animal");
            Console.WriteLine("[2] Update Animal Database");
            Console.WriteLine("[3] Display Animal Database");
            Console.WriteLine("");
            Console.WriteLine("[4] Exit Employee Menu");
        }
    }
}
