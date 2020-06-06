using System.Collections.Generic;

namespace DataCore.EntityModels
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        
        public IList<Student> Students { get; set; }
        public IList<Subject> Subjects { get; set; }
    }
}