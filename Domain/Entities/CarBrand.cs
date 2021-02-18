using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Interfaces;
using Domain.Interfaces;

namespace Domain.Entities
{
    [Table("car_brands")]
    public class CarBrand : IRegister
    {
        [Key]
        [Column]
        public int Id { get; set; }

        [Required]
        [Column]
        [MaxLength(150)]
        public string Name { get; set; }

        public List<Car> Cars { get; set; }

    }
}