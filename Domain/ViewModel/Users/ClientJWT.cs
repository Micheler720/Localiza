using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel.Users
{
    public class ClientJWT
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Cpf { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Token { get; set; }
    }
}