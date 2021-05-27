using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class TrackAppConfiguration : IEntityTypeConfiguration<TrackApp>
    {
        public void Configure(EntityTypeBuilder<TrackApp> builder)
        {
            builder.Property(s => s.Status).HasConversion(
                o => o.ToString(),
                o => (AppStatus) Enum.Parse(typeof(AppStatus), o)
            );

            //builder.HasMany(o => o.TrackAppLogs).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}