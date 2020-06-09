using System.Collections.Generic;

namespace Entity.Models.Dtos.PerformanceInfos
{
    public class SubjectPerformanceInfoDto
    {
        public int SubjectId { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSecondName { get; set; }
        public IEnumerable<StudentPerformanceDto> StudentPerformances { get; set; }
        public SubjectSettingDto SubjectSettings { get; set; }
    }
}