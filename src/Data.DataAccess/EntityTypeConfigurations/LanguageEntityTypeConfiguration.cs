using Data.DataModels.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Data.DataAccess.EntityTypeConfigurations
{
    public class LanguageEntityTypeConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> entityTypeBuilder)
        {
            entityTypeBuilder
               .HasMany(l => l.FilmProductions)
               .WithOne(fp => fp.Language)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
