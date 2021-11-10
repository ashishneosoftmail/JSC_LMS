using JSC_LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Persistence.Configurations
{
    class SuperadminConfiguration : IEntityTypeConfiguration<Superadmin>
    {
        public void Configure(EntityTypeBuilder<Superadmin> builder)
        {
            builder
               .HasKey(b => b.Id);

            builder
                .Property(b => b.Name)
                .IsRequired()
                .HasColumnName("Name").HasColumnType("nvarchar(50)");
            builder
                .Property(b => b.MobileSupport)
                .IsRequired()
                .HasColumnName("MobileSupport").HasColumnType("nvarchar(100)");
            builder
             .Property(b => b.UserId)
             .IsRequired()
             .HasColumnType("nvarchar(450)");
            builder
                .Property(b => b.EmailSupport)
                .IsRequired()
                .HasColumnName("EmailSupport").HasColumnType("nvarchar(100)");
            builder
               .Property(b => b.Logo)
               .HasColumnName("Logo").HasColumnType("varbinary(max)");
            builder
               .Property(b => b.LoginImage)
               .HasColumnName("LoginImage").HasColumnType("varbinary(max)");
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
        }
    }
}
