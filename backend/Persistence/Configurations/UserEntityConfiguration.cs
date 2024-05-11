using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;
public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder
            .HasOne(x => x.Teacher)
            .WithOne(x => x.User)
            .HasForeignKey<UserEntity>(x => x.TeacherId);
        
        builder.HasMany(x => x.Courses).WithMany();
        
        builder.Navigation(e => e.Teacher).AutoInclude();
    }
}
