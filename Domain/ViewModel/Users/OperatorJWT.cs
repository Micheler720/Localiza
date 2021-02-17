using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel.Users
{
    public class OperatorJWT
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Registration { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Token { get; set; }
    }
}