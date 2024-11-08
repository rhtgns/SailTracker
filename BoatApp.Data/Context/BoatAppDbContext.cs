using SailTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailTracker.Data.Context
{
    public class SailTrackerDbContext : DbContext
    {
        public SailTrackerDbContext(DbContextOptions<SailTrackerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NavigationFeatureConfiguration()); // Feature yerine NavigationFeature
            modelBuilder.ApplyConfiguration(new VesselConfiguration()); // Boat yerine Vessel
            modelBuilder.ApplyConfiguration(new VesselFeatureConfiguration()); // BoatFeature yerine VesselFeature
            modelBuilder.ApplyConfiguration(new SalesConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<SettingEntity>().HasData(
                new SettingEntity
                {
                    Id = 1,
                    MaintenanceMode = false // Yazım hatası düzeltildi
                });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<FeatureEntity> NavigationFeatures => Set<FeatureEntity>(); // Features yerine NavigationFeatures
        public DbSet<VesselEntity> Vessels => Set<VesselEntity>(); // Boats yerine Vessels
        public DbSet<VesselFeatureEntity> VesselFeatures => Set<VesselFeatureEntity>(); // BoatFeatures yerine VesselFeatures
        public DbSet<SalesEntity> Sales => Set<SalesEntity>();
        public DbSet<SettingEntity> Settings => Set<SettingEntity>();
    }
}

