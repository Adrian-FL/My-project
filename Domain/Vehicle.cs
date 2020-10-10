using System;
using System.Text.RegularExpressions;

namespace Sunfleet.Domain
{
    abstract class Vehicle
    {
        private string registrationNumber;
        private string model;
        private string brand;

        public Vehicle(string registrationNumber, string model, string brand)
        {
            RegistrationNumber = registrationNumber;
            Model = model;
            Brand = brand;
        }

        public string RegistrationNumber
        {
            get => registrationNumber;
            set
            {
                Regex registrationNumberRegexp = new Regex(@"([A-Z]{3})(\d{3})");

                if (!registrationNumberRegexp.IsMatch(value))
                {
                    throw new ArgumentException("Parameter registrationNumber is invalid");
                }

                registrationNumber = value;
            }
        }
        
        public string Model 
        { 
            get => model; 
            set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Parameter model cannot be null or empty");
                }
                model = value;
            }
        }

        public string Brand 
        { 
            get => brand; 
            set
            {        
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Parameter brand cannot be null or empty");
                }
                brand = value;
            }
        }
    }
}

