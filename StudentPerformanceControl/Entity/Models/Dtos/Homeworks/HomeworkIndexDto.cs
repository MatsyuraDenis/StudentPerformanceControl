using System.Collections.Generic;

namespace Entity.Models.Dtos.Homeworks
{
    public class HomeworkIndexDto
    {
        public string SubjectName { get; set; }
        public string GroupName { get; set; }
        public int TotalPoints { get; set; }
        public int GroupId { get; set; }
        public int TestExamSum { get; set; }
        public int HomeworkSum { get; set; }
        public IList<HomeworkDto> Homeworks { get; set; }
    }
}