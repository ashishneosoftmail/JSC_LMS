using JSC_LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Persistence.Configurations
{
   public class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
    {
        public void Configure(EntityTypeBuilder<Announcement> builder)
        {
            builder
               .HasKey(b => b.Id);
            builder
               .Property(b => b.AnnouncementMadeBy)
               .IsRequired()
               .HasColumnType("nvarchar(15)");
            builder
                .Property(b => b.AnnouncementTitle)
                .IsRequired()
                .HasColumnType("nvarchar(200)");
            builder
               .Property(b => b.AnnouncementContent)
               .IsRequired()
               .HasColumnType("nvarchar(max)");
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
