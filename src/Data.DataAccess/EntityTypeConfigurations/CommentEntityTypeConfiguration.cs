using Data.DataModels.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataAccess.EntityTypeConfigurations
{
    public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> entityTypeBuilder)
        {
            entityTypeBuilder
               .HasOne(c => c.ApplicationUser)
               .WithMany(au => au.Comments)
               .OnDelete(DeleteBehavior.NoAction);

            entityTypeBuilder
               .HasOne(c => c.Subtitles)
               .WithMany(s => s.Comments)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
