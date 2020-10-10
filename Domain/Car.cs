using System;
using System.Collections.Generic;
using System.Text;

namespace Sunfleet.Domain
{
    class Car : Vehicle
    {
        private TypeCar type;   // atribute - se declara imediat , private sau protected, numele e intotdeauna cu litera mica
        private bool autopilot; 


        public Car(string registrationNumber, string model, string brand, TypeCar type, bool autoPilot) : base(registrationNumber, model, brand)
        {

            Type = type;
            AutoPilot = autoPilot;
        }

        public TypeCar Type // proprety
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public bool AutoPilot 
        {

            get 
            {
                return autopilot;
            } 
            set
            {
                autopilot = value;
            }
                
        }
    }
}
