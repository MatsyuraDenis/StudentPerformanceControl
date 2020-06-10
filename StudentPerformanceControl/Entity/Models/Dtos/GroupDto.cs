using System.Collections;
using System.Collections.Generic;

namespace Entity.Models.Dtos
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Teacher { get; set; }
        public IEnumerable<SubjectDto> Subjects { get; set; }
        public IEnumerable<StudentDto> Students { get; set; }
    }
}