﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SailTracker.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailTracker.Data.Entities
{
    public class UserEntity : CoreEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; } // Yazım hatası düzeltildi
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public UserType UserType { get; set; }

        public ICollection<SalesEntity> Sales { get; set; }
        public string FİrstName { get; set; }
        public object USer { get; set; }
    }

    public class UserConfiguration : BaseConfiguration<UserEntity>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(x => x.FirstName) // Yazım hatası düzeltildi
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);
            base.Configure(builder);
        }
    }
}

