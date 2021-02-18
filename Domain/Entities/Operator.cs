using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Entities.Interfaces;
using Domain.Entities.Roles;

namespace Domain.Entities
{
    
    [Table("operators")]
    public class Operator : IUser, IOperator
    {
        [Key]
        [Column]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        [Column]
        public string Name { get; set;}

        [Required]
        [MaxLength(15)]
        [Column]
        public string Password { get; set; }
        
        [Required]
        [Column]
        public UserRole UserRole { get; set; }
        
        [MaxLength(9)]
        [Column]
        public string Registration { get; set; }

        [JsonIgnore]
        public List<Appointment> Appointments { get; set;}
    }
}