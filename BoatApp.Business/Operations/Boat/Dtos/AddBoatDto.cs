
using SailTracker.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailTracker.Business.Operations.Boat.Dtos
{
    public class AddVesselDto
    {
        public string VesselName { get; set; }           // Tekne veya gemi ismi
        public string HullModel { get; set; }            // Teknenin gövde modeli
        public int EstimatedValue { get; set; }          // Tekne değer tahmini
        public VesselTypes VesselType { get; set; }      // Tekne türü
        public List<int> NavigationFeatureIds { get; set; } // Tekneye özel navigasyon özellikleri
    }
}
