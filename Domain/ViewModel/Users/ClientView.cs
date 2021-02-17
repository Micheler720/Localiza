using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel.Users
{
    public class ClientView
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Birthay { get; set; }
        [Required]
        public string Cpf { get; set; }
    }
}