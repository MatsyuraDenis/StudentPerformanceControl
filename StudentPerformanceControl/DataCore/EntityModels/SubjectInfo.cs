using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataCore.EntityModels
{
    public class SubjectInfo
    {
        public int SubjectInfoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public IList<Subject> Subjects { get; set; }
    }
}