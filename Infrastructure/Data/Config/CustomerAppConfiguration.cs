using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class CustomerAppConfiguration : IEntityTypeConfiguration<CustomerApp>
    {
        public void Configure(EntityTypeBuilder<CustomerApp> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(p => p.AppInfo).WithMany()
                .HasForeignKey(p => p.AppInfoId);
            builder.HasOne(p => p.Customer).WithMany()
                .HasForeignKey(p => p.CustomerId);

        }
    }
}