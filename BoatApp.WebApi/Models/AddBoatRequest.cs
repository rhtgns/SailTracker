using SailTracker.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace SailTracker.WebApi.Models
{
    public class AddVesselRequest
    {
        [Required]
        public string VesselName { get; set; }          // Tekne veya gemi ismi
        [Required]
        public string HullModel { get; set; }           // Teknenin gövde modeli
        [Required]
        public int EstimatedValue { get; set; }         // Tekne değer tahmini
        [Required]
        public VesselTypes VesselType { get; set; }     // Tekne türü

        public List<int> NavigationFeatureIds { get; set; } // Tekneye özel özellik ID'leri
    }
}
