using System.Collections.Generic;

namespace Entity.Models.Dtos
{
    public class SubjectPerformanceInfoDto
    {
        public int Id { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSecondName { get; set; }
        public IEnumerable<StudentPerformanceInfo> StudentPerformances { get; set; }
    }
}