using System.Collections.Generic;

namespace DataCore.EntityModels
{
    public class SubjectInfo
    {
        public int SubjectInfoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public IList<Teacher> ProfileTeachers { get; set; }
    }
}