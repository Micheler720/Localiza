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
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public string Complement { get; set; }
        public string State { get; set; }
    }
}