using DataCore.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataCore.EntityConfigurations
{
    public class TeacherSubjectInfoEntityConfiguration : IEntityTypeConfiguration<TeacherSubjectInfo>
    {
        public void Configure(EntityTypeBuilder<TeacherSubjectInfo> builder)
        {
            builder.HasKey(tsi => new {tsi.TeacherId, tsi.SubjectInfoId});
            
            builder.HasOne(tsi => tsi.Teacher)
                .WithMany(teacher => teacher.TeacherSubjectInfos)
                .HasForeignKey(tsi => tsi.TeacherId);
            
            builder.HasOne(tsi => tsi.SubjectInfo)
                .WithMany(subjectInfo => subjectInfo.TeacherSubjectInfos)
                .HasForeignKey(tsi => tsi.SubjectInfoId);
        }
    }
}