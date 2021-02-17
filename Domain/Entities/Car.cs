using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("cars")]
    public class Car
    {
        [Key]
        [Column]
        public int Id { get; set; }

        [Required]
        [Column]
        [MaxLength(8)]
        public string Board { get; set; }

        [Required]
        [Column]
        public Double HourPrice { get; set; }

        [Required]
        [Column]
        public int LuggageCapacity { get; set; }

        [Required]
        [Column]
        public int TankCapacity { get; set; }

        [Required]
        [Column]
        public int IdBrand {get; set; }

        public CarBrand Brand { get; set; }

        [Required]
        [Column]
        public int IdCategory {get; set; }

        public CarCategory Category { get; set; }

        [Required]
        [Column]
        public int IdFuel {get; set; }

        public CarFuel Fuel { get; set; }

        [Required]
        [Column]
        public int IdModel {get; set; }

        public CarModel Model { get; set; }

        public List<Appointment> Appointments { get; set;}
    }
}