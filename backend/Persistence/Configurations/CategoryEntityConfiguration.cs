using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;
public class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.HasOne(x => x.Parent).WithMany(x => x.Categories)
            .HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.Cascade);

    }
}