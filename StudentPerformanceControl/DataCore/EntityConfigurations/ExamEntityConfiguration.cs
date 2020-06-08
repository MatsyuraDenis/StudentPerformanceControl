using DataCore.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataCore.EntityConfigurations
{
    internal class ExamEntityConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.HasOne(exam => exam.Subject)
                .WithOne(subject => subject.Exam)
                .HasForeignKey<Exam>(exam => exam.SubjectId);
        }
    }
}