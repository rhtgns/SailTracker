using System.ComponentModel.DataAnnotations;

namespace SailTracker.WebApi.Models
{
    public class RegisterRequest // Yazım hatası düzeltildi
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }
        public string FirstNAme { get; internal set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}
