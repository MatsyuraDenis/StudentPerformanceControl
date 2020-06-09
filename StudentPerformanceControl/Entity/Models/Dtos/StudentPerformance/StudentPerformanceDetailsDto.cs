using System.Collections.Generic;

namespace Entity.Models.Dtos.StudentPerformance
{
    public class StudentPerformanceDetailsDto
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }

        public IEnumerable<StudentSubjectPerformanceDto> Subjects { get; set; }
    }
}