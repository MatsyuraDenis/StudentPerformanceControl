using System.Collections.Generic;

namespace DataCore.EntityModels
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public int? GroupId { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }

        public Group Group { get; set; }
        public IList<Subject> AssignedSubjects { get; set; }
        public IList<TeacherSubjectInfo> TeacherSubjectInfos { get; set; }
    }
}