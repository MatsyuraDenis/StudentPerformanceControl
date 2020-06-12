using System.Collections;
using System.Collections.Generic;

namespace DataCore.EntityModels
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public int GroupId { get; set; }
        public int SubjectInfoId { get; set; }
        public int Module1TestMaxPoints { get; set; }
        public int Module2TestMaxPoints { get; set; }
        public int ExamMaxPoints { get; set; }
        
        public Group Group { get; set; }
        public SubjectInfo SubjectInfo { get; set; }
        public IList<StudentPerformance> StudentPerformances { get; set; }
        public IList<HomeworkInfo> HomeworkInfos { get; set; }
    }
}