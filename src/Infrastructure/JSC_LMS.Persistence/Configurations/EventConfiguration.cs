using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JSC_LMS.Domain.Entities;

namespace Scrum.Demo.Persistence.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            //Not necessary if naming conventions are followed in model
            builder
                .HasKey(b => b.EventId);

            builder
                .Property(b => b.CreatedBy)
                .HasColumnType("nvarchar(450)");

            builder
                .Property(b => b.LastModifiedBy)
                .HasColumnType("nvarchar(450)");

            builder
                .Property(b => b.Name)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder
                .Property(b => b.Artist)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder
                .Property(b => b.Description)
                .HasColumnType("nvarchar(500)")
                .IsRequired();

            builder
                .Property(b => b.ImageUrl)
                .HasColumnType("nvarchar(200)")
                .IsRequired();

        }
    }
}
