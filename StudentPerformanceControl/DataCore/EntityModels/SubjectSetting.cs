using System.Collections.Generic;

namespace DataCore.EntityModels
{
    public class SubjectSetting
    {
        public int SubjectSettingId { get; set; }
        public int SubjectId { get; set; }
        public int Module1TestMaxPoints { get; set; }
        public int Module2TestMaxPoints { get; set; }
        public int ExamMaxPoints { get; set; }

        public Subject Subject { get; set; }
        public IList<HomeworkInfo> HomeworkInfos { get; set; }
    }
}