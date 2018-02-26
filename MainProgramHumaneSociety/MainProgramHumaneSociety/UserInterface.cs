using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgramHumaneSociety
{
    class UserInterface
    {
        int userIntInput;
        public UserInterface()
        {

        }

        public int GetUserIntInput()
        {
            try
            {
                userIntInput = Convert.ToInt32(Console.ReadLine());

                if (userIntInput > 0)
                {
                    //correct! : userIntInput is a valid number
                }
                else
                {
                    Console.WriteLine("Please enter a number greater than 0");
                    userIntInput = GetUserIntInput();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Please enter a number greater than 0");
                userIntInput = GetUserIntInput();
            }
        
            return userIntInput;
        }

        public string GetUserStringInput()
        {
            return Console.ReadLine();
        }
    }
}
