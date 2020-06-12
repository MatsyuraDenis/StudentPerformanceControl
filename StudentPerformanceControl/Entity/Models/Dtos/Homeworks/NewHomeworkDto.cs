using System.ComponentModel.DataAnnotations;

namespace Entity.Models.Dtos.Homeworks
{
    public class NewHomeworkDto
    {
        public int SubjectId { get; set; }
        public int GroupId { get; set; }
        public int HomeworkNumber { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 2)]
        public string HomeworkTitle { get; set; }
        [Required]
        [Range(3, 20)]
        public int MaxPoints { get; set; }
        public NewHomeworkDataDto DataDto { get; set; }
    }
}