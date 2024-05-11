using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;
public class CommentEntityConfiguration : IEntityTypeConfiguration<CommentEntity>
{
    public void Configure(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.HasOne(x => x.Answer).WithMany()
            .HasForeignKey(x => x.AnswerId).OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(e => e.User).AutoInclude();
    }
}
