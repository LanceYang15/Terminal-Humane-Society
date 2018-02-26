using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgramHumaneSociety
{
    class Program
    {
        static void Main(string[] args)
        {
            HumanSocietyTerminal terminal = new HumanSocietyTerminal();
            terminal.ProgramStart();
            //Console.ReadKey();
        }
    }
}
