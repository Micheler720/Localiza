using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel.Users
{
    public class ClientSaveView
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Cpf { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime Birthay { get; set; }
    }
}