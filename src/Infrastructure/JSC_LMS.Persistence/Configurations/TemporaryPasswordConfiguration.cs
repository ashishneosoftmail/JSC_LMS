using JSC_LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Persistence.Configurations
{
    public class TemporaryPasswordConfiguration : IEntityTypeConfiguration<TemporaryPassword>
    {
        public void Configure(EntityTypeBuilder<TemporaryPassword> builder)
        {
            builder
               .HasKey(b => b.Id);
            builder
                .Property(b => b.Email)
                .IsRequired()
                .HasColumnType("nvarchar(100)");
            builder
               .Property(b => b.UserId)
               .IsRequired()
               .HasColumnType("nvarchar(450)");
            builder
               .Property(b => b.PasswordHash)
               .IsRequired()
               .HasColumnType("nvarchar(max)");
            builder
               .Property(b => b.IsActive)
               .IsRequired()
               .HasColumnType("bit");

            builder
                 .Property(b => b.CreatedBy)
                 .HasColumnType("nvarchar(450)");

            builder
                .Property(b => b.LastModifiedBy)
                .HasColumnType("nvarchar(450)");

            builder
                .Property(b => b.CreatedDate)
                .HasColumnType("datetime");

            builder
                .Property(b => b.LastModifiedDate)
                .HasColumnType("datetime");
        }
    }
}
