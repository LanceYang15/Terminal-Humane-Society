using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgramHumaneSociety
{
    class HumanSocietyTerminal
    {
        bool employeeWindowRunning;
        bool running;
        bool update;
        int idNumber;
        int roomNumber;
        int price;
        List<int> idBank;
        List<int> room;
        string userInputString;
        string adoptionStatus;
        string animalCategory;
        string name;        
        string shotsStatus;
        string foodPerWeek;
        SqlConnection databaseConnection;
        EmployeeInterface employeeInterface;
        TerminalInterface terminalInterface;
        UserInterface userInterface;

        public HumanSocietyTerminal()
        {
            databaseConnection = new SqlConnection("Server = .; Database=Animal; Integrated Security = true");
            employeeInterface = new EmployeeInterface();
            terminalInterface = new TerminalInterface();
            userInterface = new UserInterface();
            running = true;
            employeeWindowRunning = true;
            update = true;
            idBank = new List<int>();
            room = new List<int>();         
        }

        public void ProgramStart()
        {
            ObtainAnimalID();
            ObtainRoomNumbers();
            while (running)
            {
                Console.Clear();
                ShowFirstWindow();
                switch (userInputString)
                {
                    case "1":
                        EmployeeWindow();
                        break;

                    //case "2":
                    //    //show customer window
                    //    break;

                    case "2":
                        running = false;
                        break;

                    default:
                        break;
                }
            }
        }

        public void ObtainAnimalID()
        {
            try
            {
                databaseConnection.Open();
                SqlCommand showData = new SqlCommand("SELECT AnimalID FROM AnimalTable", databaseConnection);
                using (showData)
                {
                    SqlDataReader reader = showData.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            idBank.Add(Convert.ToInt32($"{reader["AnimalID"]}"));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed populating room numbers");
                Console.ReadKey();
            }
            databaseConnection.Close();
        }

        public void ObtainRoomNumbers()
        {
            try
            {
                databaseConnection.Open();
                SqlCommand showData = new SqlCommand("SELECT RoomNumber FROM AnimalTable", databaseConnection);
                using (showData)
                {
                    SqlDataReader reader = showData.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            room.Add(Convert.ToInt32($"{reader["RoomNumber"]}"));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed populating room numbers");
                Console.ReadKey();
            }
            databaseConnection.Close();
        }

        public void EmployeeWindow()
        {
            employeeWindowRunning = true;
            while (employeeWindowRunning)
            {
                Console.Clear();
                employeeInterface.ShowMenuSelection();
                userInputString = userInterface.GetUserStringInput();
                switch (userInputString)
                {
                    case "1":
                        AddNewAnimal();
                        break;

                    case "2":
                        UpdateDatabase();
                        break;

                    case "3":
                        ShowData();
                        Console.ReadKey();
                        break;

                    case "4":
                        employeeWindowRunning = false;
                        break;

                    default:
                        break;
                }
            }          
        }

        public void UpdateDatabase()
        {
            Console.Clear();
            ShowData();
            GetIdNumber();
            UpdateWindow();
        }

        public void UpdateWindow()
        {
            update = true;
            while (update)
            {
                GetUpdateWindowSelection();
                switch (userInputString)
                {
                    case "1":
                        UpdateAdoptionStatus();
                        break;

                    case "2":
                        UpdateShotsStatus();
                        break;

                    case "3":
                        update = false;
                        break;
                }

            }
        }

        public void UpdateShotsStatus()
        {
            try
            {
                Console.WriteLine("");
                Console.WriteLine("Please enter the new shots status");
                shotsStatus = userInterface.GetUserStringInput();
                databaseConnection.Open();
                SqlCommand updateData = new SqlCommand("UPDATE AnimalTable SET ShotsStatus = @1 WHERE AnimalID = @2", databaseConnection);
                using (updateData)
                {
                    updateData.Parameters.AddWithValue("@2", idNumber);
                    updateData.Parameters.AddWithValue("@1", shotsStatus);

                    updateData.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: UpdateShotsStatus Method");
                Console.ReadKey();
            }
        }

        public void UpdateAdoptionStatus()
        {
            try
            {
                Console.WriteLine("");
                Console.WriteLine("Please enter the new adoption status");
                adoptionStatus = userInterface.GetUserStringInput();
                databaseConnection.Open();
                SqlCommand updateData = new SqlCommand("UPDATE AnimalTable SET AdoptionStatus = @1 WHERE AnimalID = @2", databaseConnection);
                using (updateData)
                {
                    updateData.Parameters.AddWithValue("@2", idNumber);
                    updateData.Parameters.AddWithValue("@1", adoptionStatus);

                    updateData.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: UpdateAdoptionStatus Method");
                Console.ReadKey();
            }
        }

        public void GetUpdateWindowSelection()
        {
            Console.WriteLine("");
            Console.WriteLine("What would you like to update?");
            Console.WriteLine("[1] Animal Adoption Status");
            Console.WriteLine("[2] Shots Status");
            Console.WriteLine("[3] Exit");
            userInputString = userInterface.GetUserStringInput();
        }

        public void GetIdNumber()
        {
            Console.WriteLine("Please select an ID of an animal that you would like to update");
            idNumber = userInterface.GetUserIntInput();

            if (idBank.Contains(idNumber))
            {
                //correct! Continue
            }
            else
            {
                Console.WriteLine("Please enter the correct Id");
                GetIdNumber();
            }
        }

        public void ShowData()
        {
            Console.Clear();
            try
            {
                databaseConnection.Open();
                SqlCommand showData = new SqlCommand("SELECT * FROM AnimalTable", databaseConnection);
                using (showData)
                {
                    SqlDataReader reader = showData.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {                          
                            Console.WriteLine("ID: " + $"{reader["AnimalID"]}");
                            Console.WriteLine("Adoption Status: " + $"{reader["AdoptionStatus"]}");
                            Console.WriteLine("Animal Type: " + $"{reader["AnimalCategory"]}");
                            Console.WriteLine("Name: " + $"{reader["Name"]}");
                            Console.WriteLine("Room Number: " + $"{reader["RoomNumber"]} ");
                            Console.WriteLine("Shots Status: " + $"{reader["ShotsStatus"]}");
                            Console.WriteLine("Food Per Week: " + $"{reader["FoodPerWeek"]} ");
                            Console.WriteLine("Price: " + $"{reader["Price"]} ");
                            Console.WriteLine("");
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Changes have been made, saving and loading, please try again");
                Console.ReadKey();
            }
            databaseConnection.Close();
        }

        public void AddNewAnimal()
        {
            try
            {
                Console.Clear();
                GetAnimalInformation();
                databaseConnection.Open();
                SqlCommand addAnimal = new SqlCommand("INSERT INTO AnimalTable (AdoptionStatus, AnimalCategory, Name, RoomNumber, ShotsStatus, FoodPerWeek, Price) VALUES(@1, @2, @3, @4, @5, @6, @7)", databaseConnection);
                using (addAnimal)
                {
                    addAnimal.Parameters.AddWithValue("@1", adoptionStatus);
                    addAnimal.Parameters.AddWithValue("@2", animalCategory);
                    addAnimal.Parameters.AddWithValue("@3", name);
                    addAnimal.Parameters.AddWithValue("@4", roomNumber);
                    addAnimal.Parameters.AddWithValue("@5", shotsStatus);
                    addAnimal.Parameters.AddWithValue("@6", foodPerWeek);
                    addAnimal.Parameters.AddWithValue("@7", price);

                    addAnimal.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error with values");
                Console.ReadKey();
            }
                databaseConnection.Close();

        }

        public void GetAnimalInformation()
        {
            Console.WriteLine("Please enter adoption status");
            adoptionStatus = userInterface.GetUserStringInput();
            Console.WriteLine("Please enter animal category");
            animalCategory = userInterface.GetUserStringInput();
            Console.WriteLine("Please enter animal name");
            name = userInterface.GetUserStringInput();

            GetRoom();

            Console.WriteLine("Please enter shot status");
            shotsStatus = userInterface.GetUserStringInput();
            Console.WriteLine("Please enter the food per week");
            foodPerWeek = userInterface.GetUserStringInput();
            Console.WriteLine("Please enter a price for the animal");
            price = userInterface.GetUserIntInput();
        }

        public void GetRoom()
        {
            GetRoomNumber();

            if (room.Contains(roomNumber))
            {
                Console.WriteLine("Please enter another room number");
                GetRoom();
            }

            room.Add(roomNumber);
        }

        public void GetRoomNumber()
        {
            Console.WriteLine("Please enter a room number");
            roomNumber = userInterface.GetUserIntInput();
        }

        public void ShowFirstWindow()
        {
            terminalInterface.UserPersonMenu();
            userInputString = userInterface.GetUserStringInput();
        }

        //public void ShowNameFromDatabase()
        //{
        //    databaseConnection.Open();
        //    SqlCommand cmd = new SqlCommand("SELECT FirstName FROM Animal WHERE FirstName = 'Rocky' ", databaseConnection);
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        Console.WriteLine("{0}", reader.GetString(0));
        //    }
        //    reader.Close();
        //    databaseConnection.Close();
        //}
    }
}
