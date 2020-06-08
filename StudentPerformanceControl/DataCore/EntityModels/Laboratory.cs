using System.Collections.Generic;

namespace DataCore.EntityModels
{
    public class Laboratory
    {
        public int LaboratoryId { get; set; }
        public int SubjectId { get; set; }
        public int ModuleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Task { get; set; }
        public int MaxPoints { get; set; }

        public Module Module { get; set; }
        public Subject Subject { get; set; }
        public IList<StudentGrade> StudentGrades { get; set; }
    }
}