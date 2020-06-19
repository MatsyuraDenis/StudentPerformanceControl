using System.Collections.Generic;
using Entity.Models.Dtos.Subject;

namespace Entity.Models.Dtos.PerformanceInfos
{
    public class SubjectPerformanceInfoDto
    {
        public int SubjectId { get; set; }
        public int GroupType { get; set; }
        public string Name { get; set; }
        public IEnumerable<StudentPerformanceDto> StudentPerformances { get; set; }
        public SubjectDto Subject { get; set; }
    }
}