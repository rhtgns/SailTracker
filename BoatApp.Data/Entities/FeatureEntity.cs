using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailTracker.Data.Entities
{
    public class FeatureEntity : CoreEntity
    {
        public string FeatureName { get; set; } // Title yerine FeatureName

        public ICollection<VesselFeatureEntity> VesselFeatures { get; set; } // BoatFeatureEntity yerine VesselFeatureEntity
    }

    public class NavigationFeatureConfiguration : BaseConfiguration<FeatureEntity>
    {
        public override void Configure(EntityTypeBuilder<FeatureEntity> builder)
        {
            base.Configure(builder);
        }
    }
}
