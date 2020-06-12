using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entity.Models.Dtos.Homeworks;
using Entity.Models.Dtos.PerformanceInfos;

namespace Entity.Models.Dtos.Subject
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public int SubjectInfoId { get; set; }
        public int GroupId { get; set; }
        [Required]
        [Range(5,25)]
        public int Module1MaxPoints { get; set; }
        [Required]
        [Range(5,25)]
        public int Module2MaxPoints { get; set; }
        [Required]
        [Range(10,40)]
        public int ExamMaxPoints { get; set; }
        public int HomeworkPoints { get; set; }
        public int TotalPoints { get; set; }

        public string GroupName { get; set; }
        public string SubjectName { get; set; }

        public IEnumerable<HomeworkDto> HomeworkInfos { get; set; }
    }
}