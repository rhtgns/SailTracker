using System.ComponentModel.DataAnnotations;

namespace SailTracker.WebApi.Models
{
    public class NavigationFeatureRequest
    {
        [Required]
        [StringLength(30, MinimumLength = 10)] // [Length] yerine [StringLength] kullanıldı
        public string FeatureName { get; set; } // Özelliğin ismi
    }
}
