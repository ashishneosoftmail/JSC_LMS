using JSC_LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Persistence.Configurations
{
    public class EventsTableConfiguration : IEntityTypeConfiguration<EventsTable>
    {
        public void Configure(EntityTypeBuilder<EventsTable> builder)
        {
            builder
               .HasKey(b => b.Id);

            builder
                .Property(b => b.EventCoordinator)
                .IsRequired()
                .HasColumnName("EventCoordinator").HasColumnType("nvarchar(100)");
            builder
              .Property(b => b.EventTitle)
              .IsRequired()
              .HasColumnType("nvarchar(150)");
            builder
              .Property(b => b.Description)
              .IsRequired()
              .HasColumnType("nvarchar(200)");
            builder
                .Property(b => b.EventDateTime)
                .IsRequired()
                .HasColumnType("datetime");
            builder
            .Property(b => b.Venue)
            .IsRequired()
            .HasColumnType("nvarchar(100)");
            builder
           .Property(b => b.CoordinatorNumber)
           .IsRequired()
           .HasColumnType("nvarchar(10)");
            builder
             .Property(b => b.Status)
             .IsRequired()
             .HasColumnType("bit");
            builder
               .Property(b => b.Image)
               .HasColumnName("Image").HasColumnType("nvarchar(max)");
            builder
               .Property(b => b.File)
               .HasColumnName("File").HasColumnType("nvarchar(max)");
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
