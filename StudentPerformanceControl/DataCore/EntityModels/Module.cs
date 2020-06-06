using System;
using System.Collections.Generic;

namespace DataCore.EntityModels
{
    public class Module
    {
        public int ModuleId { get; set; }
        public int Number { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TestId { get; set; }

        public Test Test { get; set; }
        public IList<Laboratory> Laboratories { get; set; }
    }
}