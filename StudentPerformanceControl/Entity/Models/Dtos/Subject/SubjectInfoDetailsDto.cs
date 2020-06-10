using System.Collections.Generic;
using Entity.Models.Dtos.Teacher;

namespace Entity.Models.Dtos.Subject
{
    public class SubjectInfoDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<TeacherDto> Teachers { get; set; }
    }
}