using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("checklists")]
    public class CheckList
    {
        [Key]
        [Column]
        public int Id { get; set; }

        [Required]
        [Column]
        public bool CleanCar { get; set; }

        [Required]
        [Column]
        public bool FullTank { get; set; }

        [Required]
        [Column]
        public bool TankLightsPendant { get; set; }

        [Required]
        [Column]
        public bool Crumpled { get; set; }

        [Required]
        [Column]
        public bool Scratches { get; set; }
        
        public Appointment Appointment { get; set; }

    }
}