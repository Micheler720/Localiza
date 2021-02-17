using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel.Users
{
    public class ClientLogin
    {
        [Required]
        public string Cpf { get; set; }
        [Required]
        public string Password { get; set; }

    }
}