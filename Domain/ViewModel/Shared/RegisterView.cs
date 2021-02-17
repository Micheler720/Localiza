using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel.Shared
{
    public class RegisterView
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}