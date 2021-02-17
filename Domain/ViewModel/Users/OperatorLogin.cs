using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel.Users
{
    public class OperatorLogin
    {
        [Required]
        public string Resgistration { get; set; }
        [Required]
        public string Password { get; set; }

    }
}