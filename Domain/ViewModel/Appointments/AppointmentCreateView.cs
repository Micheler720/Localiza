using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel.Appointments
{
    public class AppointmentCreateView
    {

        [Required]
        public DateTime DateTimeExpectedCollected { get; set; }
        
        public DateTime DateTimeCollected { get; set; }

        [Required]
        public DateTime DateTimeExpectedDelivery { get; set; }        

        [Required]
        public Double HourPrice { get; set; }

        [Required]
        public int HourLocation { get; set; }

        public Double Subtotal { get; set; }

        public Double AdditionalCosts {get; set; }

        [Required]
        public Double Amount {get; set; }        

        [Required]
        public int IdClient { get; set; } 

        [Required]
        public int IdCar { get; set; } 
        [Required]
        public int IdOperator { get; set; } 
    }
}