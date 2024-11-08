
using SailTracker.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailTracker.Business.Operations.Boat.Dtos
{
    public class VesselDto
    {
        public int Id { get; set; }
        public string VesselName { get; set; }            // Tekne veya gemi ismi
        public string HullModel { get; set; }             // Teknenin gövde modeli
        public int EstimatedValue { get; set; }           // Tekne değer tahmini
        public VesselTypes VesselType { get; set; }       // Tekne türü

        public List<VesselFeatureDto> NavigationFeatures { get; set; } // Tekneye özel özellikler listesi
    }
}

