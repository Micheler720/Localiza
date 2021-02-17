using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Interfaces;
using Domain.Entities.Roles;

namespace Domain.Entities
{
    [Table("clients")]
    public class Client : IUser, IPerson
    {
        [Key]
        [Column]
        public int Id { get; set; }

        [Required]
        [Column]
        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(11)]
        [Column]
        public string Cpf { get; set; }

        public DateTime Birthay { get; set; }

        [Required]
        [MaxLength(15)]
        [Column]
        public string Password { get; set; }

        [Required]
        [Column]
        public UserRole UserRole { get; set; }
        
        public List<Appointment> Appointments { get; set;}
        
    }
}