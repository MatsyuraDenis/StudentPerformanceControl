using System.Collections.Generic;

namespace DataCore.EntityModels
{
    public class HomeworkInfo
    {
        public int HomeworkInfoId { get; set; }
        public int SubjectId { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        
        public Subject Subject { get; set; }
    }
}