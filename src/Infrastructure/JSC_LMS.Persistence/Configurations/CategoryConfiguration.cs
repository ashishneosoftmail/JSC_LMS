using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JSC_LMS.Domain.Entities;

namespace Scrum.Demo.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //Not necessary if naming conventions are followed in model
            builder
                .HasKey(b => b.Id);


            builder
                .Property(b => b.CategoryName)
                .IsRequired()
                .HasColumnType("nvarchar(50)");
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
