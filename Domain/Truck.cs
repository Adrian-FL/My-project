using System;
using System.Collections.Generic;
using System.Text;

namespace Sunfleet.Domain
{
    class Truck : Vehicle
    {
        private int capacity;
        private bool lift;

        public Truck(string registrationNumber, string model, string brand, int setCapacity, bool liFt) : base(registrationNumber, model, brand)
        {
            Capacity = setCapacity;
            Lift = liFt;
        }

        public int Capacity 
        {
           get 
            {
                return capacity;
            }
      
            set 
            {
                capacity = value;
            }
        }

        public bool Lift
        { 
            get 
            {
                return lift;
            }
            set 
            {
                lift = value;
            }
                
        }
    }
}
