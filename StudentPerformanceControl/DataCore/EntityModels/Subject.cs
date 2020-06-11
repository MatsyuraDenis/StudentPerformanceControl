using System.Collections;
using System.Collections.Generic;

namespace DataCore.EntityModels
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public int? SubjectSettingId { get; set; }
        public int GroupId { get; set; }
        public int SubjectInfoId { get; set; }
        
        public Group Group { get; set; }
        public SubjectInfo SubjectInfo { get; set; }
        public SubjectSetting SubjectSetting { get; set; }
        public IList<StudentPerformance> StudentPerformances { get; set; }
    }
}