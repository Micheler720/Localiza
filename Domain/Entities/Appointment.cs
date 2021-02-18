using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    [Table("appointments")]
    public class Appointment
    {
        [Key]
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

        [JsonIgnore]
        public Client Client { get; set; }

        [Required]
        [Column]
        public int IdCar { get; set; }
        [JsonIgnore]
        public Car Car { get; set; }

        [Required]
        [Column]
        public int IdOperator { get; set; }
        [JsonIgnore]
        public Operator Operator { get; set; }

        [Column]
        public int? IdCheckList { get; set; }
        [JsonIgnore]
        public CheckList CheckList { get; set; }
    }
}