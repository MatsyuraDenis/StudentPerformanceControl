using System.Collections.Generic;

namespace Entity.Models.Dtos
{
    public class StudentPerformanceInfo
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentSecond { get; set; }
        public IList<StudentHomeworkDto> Homeworks { get; set; }
    }
}