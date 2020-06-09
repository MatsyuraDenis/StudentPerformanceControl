using DataCore.EntityConfigurations;
using DataCore.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace DataCore.Contexts
{
    public class SPCContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentPerformance> StudentGrades { get; set; }
        public DbSet<SubjectInfo> SubjectInfos { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StudentPerformance> StudentPerformances { get; set; }
        public DbSet<HomeworkResult> HomeworkResults { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-7L0U503;Initial Catalog=StudentPerformanceControl;User ID=dbo;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GroupEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherSubjectInfoEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}