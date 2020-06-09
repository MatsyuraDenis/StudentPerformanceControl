using DataCore.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataCore.EntityConfigurations
{
    public class SubjectSettingEntityConfiguration : IEntityTypeConfiguration<SubjectSetting>
    {
        public void Configure(EntityTypeBuilder<SubjectSetting> builder)
        {
            builder.HasOne(setting => setting.Subject)
                .WithOne(subject => subject.SubjectSetting)
                .HasForeignKey<SubjectSetting>(setting => setting.SubjectId);
        }
    }
}