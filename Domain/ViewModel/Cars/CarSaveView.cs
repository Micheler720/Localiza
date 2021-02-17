using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel.Cars
{
    public class CarSaveView
    {
        [Required]
        public string Board { get; set; }
        [Required]
        public Double HourPrice { get; set; }
        [Required]
        public int LuggageCapacity { get; set; }
        [Required]
        public int TankCapacity { get; set; }
        [Required]
        public int IdBrand {get; set; }
        [Required]
        public int IdCategory {get; set; }
        [Required]
        public int IdFuel {get; set; }
        [Required]
        public int IdModel {get; set; }
    }
}