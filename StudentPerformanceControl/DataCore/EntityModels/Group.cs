using System;
using System.Collections.Generic;

namespace DataCore.EntityModels
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int GroupTypeId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeactivatedAt { get; set; }
        
        public GroupType GroupType { get; set; }
        public IList<Student> Students { get; set; }
        public IList<Subject> Subjects { get; set; }
    }
}