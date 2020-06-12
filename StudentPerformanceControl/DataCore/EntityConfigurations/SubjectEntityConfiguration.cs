using DataCore.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataCore.EntityConfigurations
{
    internal class SubjectEntityConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasOne(subject => subject.SubjectInfo)
                .WithMany(info => info.Subjects)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(subject => subject.Group)
                .WithMany(group => group.Subjects)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(subject => subject.StudentPerformances)
                .WithOne(performance => performance.Subject)
                .HasForeignKey(performance => performance.SubjectId);
            
            builder.HasMany(subject => subject.HomeworkInfos)
                .WithOne(homework => homework.Subject)
                .HasForeignKey(homework => homework.SubjectId);
        }
    }
}