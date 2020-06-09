using DataCore.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataCore.EntityConfigurations
{
    public class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasOne(student => student.Group)
                .WithMany(group => group.Students)
                .HasForeignKey(student => student.GroupId);

            builder.HasMany(student => student.StudentPerformances)
                .WithOne(performance => performance.Student)
                .HasForeignKey(performance => performance.StudentId);
        }
    }
}