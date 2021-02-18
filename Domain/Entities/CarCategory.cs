using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Interfaces;
using Domain.Interfaces;

namespace Domain.Entities
{
    [Table("car_categories")]
    public class CarCategory : IRegister
    {
        [Key]
        [Column]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        [Column]
        public string Name { get; set; }
        public List<Car> Cars { get; set; }

    }
}