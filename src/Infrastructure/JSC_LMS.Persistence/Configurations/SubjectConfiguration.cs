using JSC_LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Persistence.Configurations
{
  public  class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder
          .HasKey(b => b.Id);


            builder
                .Property(b => b.SubjectName)
                .IsRequired()
                .HasColumnType("nvarchar(150)");

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
