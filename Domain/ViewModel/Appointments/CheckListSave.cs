using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel.Appointments
{
    public class CheckListSave
    {
        [Required]
        public DateTime DateTimeDelivery { get; set; }

        [Required]
        public bool Inspected { get; set; } 

        [Required]
        public bool CleanCar { get; set; }

        [Required]
        public bool FullTank { get; set; }

        [Required]
        public bool TankLightsPendant { get; set; }

        [Required]
        public bool Crumpled { get; set; }

        [Required]
        public bool Scratches { get; set; }

        
        
    }
}