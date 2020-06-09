using DataCore.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataCore.EntityConfigurations
{
    public class HomeworkResultEntityConfiguration : IEntityTypeConfiguration<HomeworkResult>
    {
        public void Configure(EntityTypeBuilder<HomeworkResult> builder)
        {
            builder.HasOne(result => result.StudentPerformance)
                .WithMany(performance => performance.HomeworkResults)
                .HasForeignKey(result => result.StudentPerformanceId);
        }
    }
}