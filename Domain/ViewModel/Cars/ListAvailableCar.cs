using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModel.Cars
{
    public class ListAvailableCar
    {
        public int IdCar { get; set; }
        public string Board { get; set; }
        public Double HourPrice { get; set; }
        public int LuggageCapacity { get; set; }
        public int TankCapacity { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Fuel { get; set; }
        public string Model { get; set; }
        public DateTime DateTimeExpectedNextCollected { get; set; }
    }
}
