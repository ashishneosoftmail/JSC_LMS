using JSC_LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Persistence.Configurations
{
   public class GallaryConfiguration : IEntityTypeConfiguration<Gallary>
    {
        public void Configure(EntityTypeBuilder<Gallary> builder)
        {
            builder
              .HasKey(b => b.Id);

            builder
              .Property(b => b.FileName)
              .IsRequired()
              .HasColumnName("FileName").HasColumnType("nvarchar(100)");

           
            builder
               .Property(b => b.image)
               .HasColumnName("Image").HasColumnType("nvarchar(max)");
            builder
               .Property(b => b.FileType)
               .HasColumnName("FileType").HasColumnType("nvarchar(max)");

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
