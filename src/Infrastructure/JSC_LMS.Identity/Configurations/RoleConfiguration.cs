using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace JSC_LMS.Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = "Parent",
                    NormalizedName = "PARENT"
                },
                new IdentityRole
                {
                    Name = "Student",
                    NormalizedName = "STUDENT"
                },
                new IdentityRole
                {
                    Name = "Teacher",
                    NormalizedName = "TEACHER"
                },
                new IdentityRole
                {
                    Name = "Principal",
                    NormalizedName = "PRINCIPAL"
                },
                new IdentityRole
                {
                    Name = "Institute Admin",
                    NormalizedName = "INSTITUTE ADMIN"
                },
                 new IdentityRole
                 {
                     Name = "Super administrator",
                     NormalizedName = "SUPER ADMINISTRATOR"
                 }
            );
        }
    }
}
