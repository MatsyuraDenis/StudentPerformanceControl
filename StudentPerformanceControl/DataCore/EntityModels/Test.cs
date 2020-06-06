using System.Collections;
using System.Collections.Generic;

namespace DataCore.EntityModels
{
    public class Test
    {
        public int TestId { get; set; }

        public IList<StudentGrade> StudentGrades { get; set; }
    }
}