using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;
public class EpisodeEntityConfiguration : IEntityTypeConfiguration<EpisodeEntity>
{
    public void Configure(EntityTypeBuilder<EpisodeEntity> builder)
    {
        builder.HasOne(x => x.Course).WithMany(x => x.Episodes)
            .HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Cascade);

    }
}