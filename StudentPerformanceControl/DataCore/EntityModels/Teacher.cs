using System.Collections.Generic;

namespace DataCore.EntityModels
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }

        public IList<Subject> AssignedSubjects { get; set; }
    }
}