using System.Collections.Generic;

namespace DataCore.EntityModels
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public int GroupId { get; set; }
        public SubjectInfo SubjectInfo { get; set; }
        public IList<Module> Modules { get; set; }
        
        public Teacher Teacher { get; set; }
        public Group Group { get; set; }
    }
}