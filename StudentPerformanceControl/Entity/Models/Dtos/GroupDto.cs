using System.Collections;
using System.Collections.Generic;

namespace Entity.Models.Dtos
{
    public class GroupDto
    {
        public int Id { get; set; }
        public IList<SubjectDto> Subjects { get; set; }
        public IList<StudentDto> Students { get; set; }
    }
}