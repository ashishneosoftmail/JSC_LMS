using JSC_LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            //Not necessary if naming conventions are followed in model
            builder
                .HasKey(b => b.Id);

            builder
                .Property(b => b.RoleName)
                .IsRequired()
                .HasColumnName("RoleName");
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
