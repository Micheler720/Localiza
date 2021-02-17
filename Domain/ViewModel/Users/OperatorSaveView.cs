using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel.Users
{
    public class OperatorSaveView
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Registration { get; set; }
        [Required]
        public string Password { get; set; }
    }
}