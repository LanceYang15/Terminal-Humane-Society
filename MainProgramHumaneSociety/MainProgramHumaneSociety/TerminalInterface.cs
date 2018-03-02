using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgramHumaneSociety
{
    class TerminalInterface
    {
        public TerminalInterface()
        {

        }

        public void UserPersonMenu()
        {
            //Console.WriteLine("Log in as:");
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("[1] Employee Sign In");
            //Console.WriteLine("[2] Customer");
            Console.WriteLine("");
            Console.WriteLine("[2] Exit Program");
        }
    }
}
