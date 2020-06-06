using DataCore.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataCore.EntityConfigurations
{
    internal class SubjectEntityConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasOne(subject => subject.Teacher)
                .WithMany(teacher => teacher.AssignedSubjects)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}