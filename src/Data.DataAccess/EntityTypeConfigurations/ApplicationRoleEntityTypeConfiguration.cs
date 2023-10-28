using Data.DataModels.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Data.DataAccess.EntityTypeConfigurations
{
    public class ApplicationRoleEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasMany(e => e.ApplicationUserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
