using System.ComponentModel.DataAnnotations;

namespace Entity.Models.Dtos.Homeworks
{
    public class NewHomeworkDto
    {
        public int SubjectId { get; set; }
        public int GroupId { get; set; }
        public int HomeworkNumber { get; set; }
        public string HomeworkTitle { get; set; }
        [Range(3,15)]
        public int MaxPoints { get; set; }
        public NewHomeworkDataDto DataDto { get; set; }
    }
}