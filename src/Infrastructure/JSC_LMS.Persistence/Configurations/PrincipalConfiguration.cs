using JSC_LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Persistence.Configurations
{

    public class PrincipalConfiguration : IEntityTypeConfiguration<Principal>
    {
        public void Configure(EntityTypeBuilder<Principal> builder)
        {
            //Not necessary if naming conventions are followed in model
            builder
                .HasKey(b => b.Id);
            builder
                .Property(b => b.AddressLine1)
                .IsRequired()
                .HasColumnType("nvarchar(100)");
            builder
                .Property(b => b.AddressLine2)
                .IsRequired()
                .HasColumnType("nvarchar(100)");
            builder
                .Property(b => b.Mobile)
                .IsRequired()
                .HasColumnType("nvarchar(20)");
            builder
                .Property(b => b.UserId)
                .IsRequired()
                .HasColumnType("nvarchar(450)");
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
            /*  builder
               .HasOne(b => b.State)
               .WithMany(b => b.Institute)
               .HasForeignKey(b => b.StateId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
              builder
               .HasOne(b => b.City)
               .WithMany(b => b.Institute)
               .HasForeignKey(b => b.CityId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
              builder
             .HasOne(b => b.Zip)
             .WithMany(b => b.Institute)
             .HasForeignKey(b => b.ZipId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);*/
        }
    }
}