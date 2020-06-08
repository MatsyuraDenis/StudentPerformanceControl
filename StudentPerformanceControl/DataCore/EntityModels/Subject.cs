using System.Collections;
using System.Collections.Generic;

namespace DataCore.EntityModels
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public int GroupId { get; set; }
        public int ExmaId { get; set; }
        public int SubjectInfoId { get; set; }
        
        public Exam Exam { get; set; }
        public Teacher Teacher { get; set; }
        public Group Group { get; set; }
        public SubjectInfo SubjectInfo { get; set; }
        public IList<Module> Modules { get; set; }

    }
}