using System.Collections.Generic;

namespace Entity.Models.Dtos.PerformanceInfos
{
    public class SubjectSettingDto
    {
        public int SubjectSettingId { get; set; }
        public int Module1MaxPoints { get; set; }
        public int Module2MaxPoints { get; set; }
        public int ExamMaxPoint { get; set; }
        public IEnumerable<HomeworkSettingDto> HomeworkSettings { get; set; }
    }
}