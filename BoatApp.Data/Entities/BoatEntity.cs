using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SailTracker.Data.Enums;

namespace SailTracker.Data.Entities
{
    public class VesselEntity : CoreEntity
    {
        public string HullModel { get; set; } // Model yerine HullModel
        public string VesselName { get; set; } // Name yerine VesselName
        public int EstimatedValue { get; set; } // Price yerine EstimatedValue
        public VesselTypes VesselType { get; set; } // BoatTypes yerine VesselTypes

        public ICollection<VesselFeatureEntity> VesselFeatures { get; set; } // BoatFeatures yerine VesselFeatures
        public ICollection<SalesEntity> Sales { get; set; }
    }

    public class VesselConfiguration : BaseConfiguration<VesselEntity>
    {
        public override void Configure(EntityTypeBuilder<VesselEntity> builder)
        {
            builder.Property(x => x.VesselName)
                .IsRequired()
                .HasMaxLength(80);
            base.Configure(builder);
        }
    }
}




