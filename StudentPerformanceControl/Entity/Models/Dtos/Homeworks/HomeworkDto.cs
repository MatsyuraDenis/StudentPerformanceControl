using System.ComponentModel.DataAnnotations;

namespace Entity.Models.Dtos.Homeworks
{
    public class HomeworkDto
    {
        public int HomeworkId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int GroupId { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 3)]
        public string HomeworkTitle { get; set; }
        public int Number { get; set; }
        [Required]
        [Range(3,20)]
        public int MaxPoints { get; set; }
    }
}