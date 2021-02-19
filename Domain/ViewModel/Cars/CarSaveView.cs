using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        public int Year { get; set; }
        [Required]
        public int IdModel {get; set; }

        [Required]
        public List<string> Images { get; set; }

        [JsonIgnore]
        public string Photos { get; set; }
    }
}