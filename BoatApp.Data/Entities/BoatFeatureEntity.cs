using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailTracker.Data.Entities
{
    public class VesselFeatureEntity : CoreEntity
    {
        public int VesselId { get; set; } // BoatId yerine VesselId
        public int NavigationFeatureId { get; set; } // FeatureId yerine NavigationFeatureId

        public VesselEntity Vessel { get; set; } // Boat yerine Vessel
        public FeatureEntity NavigationFeature { get; set; } // Feature yerine NavigationFeature
        public int FeatureId { get; set; }
        public object Feature { get; set; }
    }

    public class VesselFeatureConfiguration : BaseConfiguration<VesselFeatureEntity>
    {
        public override void Configure(EntityTypeBuilder<VesselFeatureEntity> builder)
        {
            builder.Ignore(x => x.Id);
            builder.HasKey("VesselId", "NavigationFeatureId"); // BoatId, FeatureId yerine VesselId, NavigationFeatureId
            base.Configure(builder);
        }
    }
}
