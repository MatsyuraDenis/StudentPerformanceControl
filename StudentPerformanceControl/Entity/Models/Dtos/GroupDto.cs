using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity.Models.Dtos
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeactivatedAt { get; set; }
        public IEnumerable<SubjectDto> Subjects { get; set; }
        public IEnumerable<StudentDto> Students { get; set; }

        public GroupDto()
        {
            Subjects = new List<SubjectDto>();
            Students = new List<StudentDto>();
        }
    }
}