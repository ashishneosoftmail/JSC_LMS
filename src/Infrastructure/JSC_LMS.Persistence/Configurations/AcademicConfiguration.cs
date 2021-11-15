using JSC_LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Persistence.Configurations
{

    public class AcademicConfiguration : IEntityTypeConfiguration<Academic>
    {
        public void Configure(EntityTypeBuilder<Academic> builder)
        {
            builder
               .HasKey(b => b.Id);
            builder
               .Property(b => b.Type)
               .IsRequired()
               .HasColumnType("nvarchar(100)");
            builder
               .Property(b => b.CutOff)
               .IsRequired()
               .HasColumnType("decimal(18,7)");
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
