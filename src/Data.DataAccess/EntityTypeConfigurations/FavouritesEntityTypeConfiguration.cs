using Data.DataModels.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Data.DataAccess.EntityTypeConfigurations
{
    public class FavouritesEntityTypeConfiguration : IEntityTypeConfiguration<Favourites>
    {
        public void Configure(EntityTypeBuilder<Favourites> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasKey(f => new { f.ApplicationUserId, f.SubtitlesId });
            entityTypeBuilder
                .HasOne(f => f.ApplicationUser)
                .WithMany(au => au.Favourites)
                .HasForeignKey(f => f.ApplicationUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            entityTypeBuilder
                .HasOne(f => f.Subtitles)
                .WithMany(s => s.Favourites)
                .HasForeignKey(f => f.SubtitlesId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
