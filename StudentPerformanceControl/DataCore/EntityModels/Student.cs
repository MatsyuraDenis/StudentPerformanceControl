using System.Collections.Generic;

namespace DataCore.EntityModels
{
    public class Student
    {
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        
        public Group Group { get; set; }
        public IList<StudentGrade> StudentGrades { get; set; }
    }
}