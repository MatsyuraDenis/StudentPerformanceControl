using DataCore.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataCore.EntityConfigurations
{
    public class HomeworkInfoEntityConfiguration : IEntityTypeConfiguration<HomeworkInfo>
    {
        public void Configure(EntityTypeBuilder<HomeworkInfo> builder)
        {
        }
    }
}