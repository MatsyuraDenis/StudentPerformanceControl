using System.Collections.Generic;
using Entity.Models.Dtos.Homeworks;

namespace Entity.Models.Dtos.PerformanceInfos
{
    public class SubjectSettingDto
    {
        public int SubjectSettingId { get; set; }
        public int Module1MaxPoints { get; set; }
        public int Module2MaxPoints { get; set; }
        public int ExamMaxPoint { get; set; }
        public IEnumerable<HomeworkDto> HomeworkInfos { get; set; }
    }
}