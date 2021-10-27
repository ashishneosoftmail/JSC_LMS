using JSC_LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Persistence.Configurations
{
    public class ZipConfiguration : IEntityTypeConfiguration<Zip>
    {
        public void Configure(EntityTypeBuilder<Zip> builder)
        {

            builder
                .HasKey(b => b.Id);
            builder
                .Property(b => b.Zipcode)
                .IsRequired()
                .HasColumnType("nvarchar(10)");
            builder
                .Property(b => b.IsActive)
                .IsRequired()
                .HasColumnType("bit");

            builder
                 .Property(b => b.CreatedBy)
                 .HasColumnType("int");

            builder
                .Property(b => b.LastModifiedBy)
                .HasColumnType("int");

            builder
                .Property(b => b.DeletedBy)
                .HasColumnType("int");
            builder
                .Property(b => b.CreatedDate)
                .HasColumnType("datetime");

            builder
                .Property(b => b.LastModifiedDate)
                .HasColumnType("datetime");
            builder
              .Property(b => b.DeletedDate)
              .HasColumnType("datetime");
            builder
             .HasOne(b => b.City)
             .WithMany(b => b.Zip)
             .HasForeignKey(b => b.CityId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
