using DataCore.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataCore.EntityConfigurations
{
    public class StudentPerformanceEntityConfiguration : IEntityTypeConfiguration<StudentPerformance>
    {
        public void Configure(EntityTypeBuilder<StudentPerformance> builder)
        {
            builder.HasOne(performance => performance.Subject)
                .WithMany(subject => subject.StudentPerformances)
                .HasForeignKey(performance => performance.SubjectId);

            builder.HasOne(performance => performance.Student)
                .WithMany(student => student.StudentPerformances)
                .HasForeignKey(performance => performance.StudentId);
        }
    }
}