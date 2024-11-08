using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailTracker.Data.Entities
{
    public class SalesEntity : CoreEntity
    {
        public int VesselId { get; set; } // BoatId yerine VesselId
        public int UserId { get; set; }
        public DateTime SellDate { get; set; }

        public UserEntity User { get; set; }
        public VesselEntity Vessel { get; set; } // Boat yerine Vessel
    }

    public class SalesConfiguration : BaseConfiguration<SalesEntity>
    {
        public override void Configure(EntityTypeBuilder<SalesEntity> builder)
        {
            base.Configure(builder);
        }
    }
}
