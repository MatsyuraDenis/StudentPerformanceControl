using DataCore.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataCore.EntityConfigurations
{
    public class GroupEntityConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasOne(group => group.Curator)
                .WithOne(teacher => teacher.Group)
                .HasForeignKey<Group>(group => group.CuratorId);

            builder.HasMany(group => group.Students)
                .WithOne(student => student.Group)
                .HasForeignKey(student => student.GroupId);

            builder.HasMany(group => group.Subjects)
                .WithOne(subject => subject.Group)
                .HasForeignKey(subject => subject.GroupId);
        }
    }
}