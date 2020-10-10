using Sunfleet.Domain;
using System;
using System.Linq;
using System.Threading;
using static System.Console;


namespace Sunfleet
{



    class Program
    {
        static void Main(string[] args)
        {
            UserInterface userInterface = new UserInterface();
            userInterface.Login();
        }
    }
}

