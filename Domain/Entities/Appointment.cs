using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("appointments")]
    public class Appointment
    {
        [KeyAttribute]
        public int Id { get; set; }

        [Required]
        [Column]
        public DateTime Schedule { get; set; }

        [Required]
        [Column]
        public DateTime DateTimeExpectedCollected { get; set; }

        [Column]
        public DateTime? DateTimeCollected { get; set; }

        [Required]
        [Column]
        public DateTime DateTimeExpectedDelivery { get; set; }

        public DateTime? DateTimeDelivery { get; set; }

        [Required]
        [Column]
        public Double HourPrice { get; set; }

        [Required]
        [Column]
        public int HourLocation { get; set; }

        [Column]
        public Double Subtotal { get; set; }

        [Column]
        public Double AdditionalCosts {get; set; }

        [Required]
        [Column]
        public Double Amount {get; set; }

        [Required]
        [Column]
        public bool Inspected { get; set; }   

        [Required]
        [Column]
        public int IdClient { get; set; }   

        public Client Client { get; set; }

        [Required]
        [Column]
        public int IdCar { get; set; } 

        public Car Car { get; set; }

        [Required]
        [Column]
        public int IdOperator { get; set; }  

        public Operator Operator { get; set; }

        [Column]
        public int? IdCheckList { get; set; }
        public CheckList CheckList { get; set; }
    }
}