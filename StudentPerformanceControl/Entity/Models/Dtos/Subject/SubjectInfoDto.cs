using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity.Models.Dtos.Subject
{
    public class SubjectInfoDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 3)]
        public string Title { get; set; }
        public int GroupLearn { get; set; }
        public int GroupLearned { get; set; }
    }
}