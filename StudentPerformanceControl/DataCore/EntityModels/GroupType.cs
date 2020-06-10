using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataCore.EntityModels
{
    public class GroupType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public IEnumerable<Group> Groups { get; set; }
    }
}