using DataCore.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace DataCore.Contexts
{
    public class SPCContext : DbContext
    {
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Laboratory> Laboratories { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentGrade> StudentGrades { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectInfo> SubjectInfos { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Test> Tests { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-7L0U503;Initial Catalog=StudentPerformanceControl;User ID=dbo;Trusted_Connection=True;");
        }
    }
}