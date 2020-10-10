using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using static System.Console;

namespace Sunfleet.Domain
{
    public class UserInterface
    {

        Dictionary<string, object> myVehicles =
        new Dictionary<string, object>();

// comment
        private string user = "boss";
        private string pass = "fleet";
        private bool userIsLoggedIn = false;



        public void Login()
        {
            bool access;
            do
            {
                CursorVisible = true;
                WriteLine("Username: ");
                WriteLine("Password: ");
                SetCursorPosition(10, 0);
                string inputUser = ReadLine();
                SetCursorPosition(10, 1);
                string inputPass = ReadLine();
                access = inputUser == user && inputPass == pass;
                Clear();
                if (!access)
                {
                    CursorVisible = false;
                    WriteLine("Invalid credentials, please try again ");
                    ReadKey(true);
                    Thread.Sleep(2000);
                    Clear();
                }
            }
            while (!access);

            userIsLoggedIn = true;

            Clear();

            do
            {
                CursorVisible = false;
                DisplayMenu();
                ConsoleKeyInfo keyPressed = ReadKey(true);
                Clear();

                switch (keyPressed.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        DisplayMenuCars();
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        SearchVehicles();
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        LogOut();
                        break;
                }
            } while (userIsLoggedIn);
        }

        private void SearchVehicles()
        {
            Clear();
            WriteLine("Registration number: ");
            string registrationNumber = ReadLine();
            bool vehicleExists = false;
              
            foreach (KeyValuePair<string, object> entry in myVehicles)
            {
                if (entry.Key == registrationNumber)
                {
                    if (typeof(Car).IsInstanceOfType(entry.Value))
                    {
                        Car myCar = (Car)entry.Value;
                        WriteLine("Registration number: " + myCar.RegistrationNumber);
                        WriteLine("Brand: " + myCar.Brand);
                        WriteLine("Model: " + myCar.Model);
                        WriteLine("Type: " + myCar.Type);
                        if (myCar.AutoPilot == true)
                        {
                            WriteLine("Autopilot: Yes");
                        }
                        else
                        {
                            WriteLine("Autopilot: No");
                        }
                        vehicleExists = true;
                        break;
                    }

                    if (typeof(Truck).IsInstanceOfType(entry.Value))
                    {
                        Truck myTruck = (Truck)entry.Value;
                        WriteLine("Registration number: " + myTruck.RegistrationNumber);
                        WriteLine("Brand: " + myTruck.Brand);
                        WriteLine("Model: " + myTruck.Model);
                        WriteLine("Capacity:" + myTruck.Capacity);

                        if (myTruck.Lift == true)
                        {
                            WriteLine("Lift: Yes");

                        }
                        else
                        {
                            WriteLine("Lift: No");
                        }
                        vehicleExists = true;
                        break;
                    }

                 
                }

            }

            if (vehicleExists == false)
            {
                Clear();
                WriteLine("Vehicle not found! ");
                Thread.Sleep(2000);
                Clear();
                DisplayMenu();
            }
            exitMenuOption();
        }
        static void exitMenuOption()
        {
            CursorVisible = false;
            WriteLine("");

            WriteLine("Press any key to close the window...");
            ConsoleKeyInfo keyPressed = ReadKey(true);

            switch (keyPressed.Key)
            {
                default:
                    Clear();
                    break;
            }
        }

        private void LogOut()
        {
            Login();
        }


        private void DisplayMenu()
        {
            Clear();
            WriteLine("1. Add vehicle ");
            WriteLine("2. Search vehicle ");
            WriteLine("3. Logout ");
        }

        private void DisplayMenuCars()
        {
            Clear();
            bool shouldRun = true;
            while (shouldRun)
            {
                Clear();
                WriteLine("1. Car");
                WriteLine("2. Truck");

                ConsoleKeyInfo keyPressed = ReadKey(true);

                switch (keyPressed.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        AddCar();
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        AddTruck();
                        break;

                    default:
                        break;
                }
                shouldRun = false;
            }
        }

        private void AddCar()
        {
            CursorVisible  = true;
            Clear();
            
            WriteLine("Registration number: ");
            WriteLine("Brand: ");
            WriteLine("Model: ");
            WriteLine("Type (Sedan, Compact, Subcompact): ");
            WriteLine("Autopilot(Yes/No): ");
            SetCursorPosition(21, 0);
            string registrationNumber = ReadLine();
            SetCursorPosition(7, 1);
            string brand = ReadLine();
            SetCursorPosition(8, 2);
            string model = ReadLine();
            SetCursorPosition(34, 3);
            string typeCar = ReadLine();
            SetCursorPosition(23, 4);
            string autoPilotAnswer = ReadLine();
            bool autopilot = false;


            WriteLine("Is this correct? (Y)es/(N)o ");
            string answer = ReadLine();

            switch (answer)          // switch se foloseste cand raspunsul se poate intui
            {
                case "Y":
                    if (String.IsNullOrEmpty(brand) == false)
                    {
                        if (String.IsNullOrEmpty(model) == false)
                        {
                            try
                            {
                                TypeCar type = (TypeCar)Enum.Parse(typeof(TypeCar), typeCar);
                                switch (autoPilotAnswer)
                                {
                                    case "Yes":

                                        autopilot = true;
                                        break;

                                    case "No":

                                        autopilot = false;

                                        break;

                                    default:
                                        Clear();
                                        WriteLine("Invalid option for autopilot, please try again!");
                                        Thread.Sleep(2000);
                                        Clear();
                                        break;
                                }

                                try
                                {
                                  bool exists =  CheckVehicle(registrationNumber);
                                    if (exists == true)
                                    {
                                        Clear();
                                        WriteLine("Vehicle already exists! ");
                                        Thread.Sleep(2000);
                                        Clear();
                                        AddCar();
                                    }
                                    else
                                    {
                                        Car myCar = new Car(registrationNumber, model, brand, type, autopilot);
                                        myVehicles.Add(registrationNumber,myCar);
                                        Clear();
                                        WriteLine("Vehicle registred! ");
                                        Thread.Sleep(2000);
                                        Clear();
                                        DisplayMenu();
                                    }
                                }
                                catch (ArgumentException e)
                                {
                                    Clear();
                                    WriteLine(e.Message + "[typecar]");
                                    Thread.Sleep(2000);
                                    Clear();
                                    AddCar();
                                }

                            }
                            catch (Exception)
                            {
                                Clear();
                                WriteLine("Invalid type, please try again!");
                                Thread.Sleep(2000);
                                Clear();
                                AddCar();
                            }

                        }
                        else
                        {
                            Clear();
                            WriteLine("Invalid model, please try again! ");
                            Thread.Sleep(2000);
                            Clear();
                            AddCar();
                        }
                    }
                    else
                    {
                        Clear();
                        WriteLine("Invalid brand, please try again! ");
                        Thread.Sleep(2000);
                        Clear();
                        AddCar();
                    }

                    break;

                case "N":
                    Clear();
                    AddCar();
                    break;

                default:
                    Clear();
                    AddCar();
                    break;
            }

        }

        private void AddTruck()
        {
            CursorVisible = true;
            Clear();

            WriteLine("Registration number: ");
            WriteLine("Brand: ");
            WriteLine("Model: ");
            WriteLine("Capacity: ");
            WriteLine("Lift Yes / No : ");
            SetCursorPosition(21, 0);
            string registrationNumber = ReadLine();
            SetCursorPosition(7, 1);
            string brand = ReadLine();
            SetCursorPosition(8, 2);
            string model = ReadLine();
            SetCursorPosition(10, 3);
            string capacity = ReadLine();
            SetCursorPosition(15, 4);
            string liftAnswer = ReadLine();
            bool lift = false;

            WriteLine("Is this correct? (Y)es/(N)o ");
            string answer = ReadLine();

            switch (answer)
            {
                case "Y":

                    if (String.IsNullOrEmpty(brand) == false)
                    { 
                        if (string.IsNullOrEmpty(model) == false) 
                        {
                            try
                            {
                                int truckCapacity = Convert.ToInt32(capacity);

                                switch (liftAnswer)
                                {

                                    case "Yes":
                                        lift = true;

                                        break;

                                    case "No":

                                        lift = false;

                                        break;

                                    default:
                                        break;
                                }
                                
                                try
                                {
                                    bool exists = CheckVehicle(registrationNumber);

                                    if (exists == true)
                                    {
                                        Clear();
                                        WriteLine("Vehicle already exists! ");
                                        Thread.Sleep(200);
                                        Clear();
                                        AddTruck();

                                    }
                                    else
                                    {
                                        Truck myTruck = new Truck(registrationNumber, model, brand, truckCapacity, lift);
                                        myVehicles.Add(registrationNumber, myTruck);
                                        Clear();
                                        WriteLine("Vehicle registred! ");
                                        Thread.Sleep(2000);
                                        Clear();
                                        DisplayMenu();
                                    }
                                }
                                catch (ArgumentException e)
                                {
                                    Clear();
                                    WriteLine(e.Message);
                                    Thread.Sleep(2000);
                                    Clear();
                                    AddTruck();
                                }

                            }
                            catch (FormatException e)
                            {
                                Clear();
                                WriteLine(e.Message);
                                Thread.Sleep(2000);
                                Clear();
                                AddTruck();
                            }
                        }
                        else
                        {
                            Clear();
                            WriteLine("Model cannot be null or empty!  ");
                            Thread.Sleep(2000);
                            Clear();
                            AddTruck();
                        }
                    }
                    else
                    {
                        Clear();
                        WriteLine("Brand cannot be null or empty  ");
                        Thread.Sleep(2000);
                        Clear();
                        AddTruck();
                    }

                    break;

                case "N":
                    Clear();
                    AddTruck();
                    break;

                default:
                    Clear();
                    AddTruck();
                    break;
            }

        }

        private bool CheckVehicle(string registrationNumber)
        {
            bool exists = false;

            Regex registrationNumberRegexp = new Regex(@"([A-Z]{3})(\d{3})");

            if (!registrationNumberRegexp.IsMatch(registrationNumber))
            {
                throw new ArgumentException("Parameter registrationNumber is invalid");
            }
            else
            {
                   foreach (KeyValuePair<string, object> entry in myVehicles) //entry pereche  - cheie,valaoare
                   {
                    // do something with entry.Value or entry.Key
                    
                    
                       if (entry.Key == registrationNumber)
                       {
                        exists = true;
                        break;
                       }
                   }
                
            }
            return exists;

        }
    }
}
