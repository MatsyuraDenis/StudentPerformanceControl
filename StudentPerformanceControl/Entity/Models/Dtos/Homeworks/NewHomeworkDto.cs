namespace Entity.Models.Dtos.Homeworks
{
    public class NewHomeworkDto
    {
        public int SubjectId { get; set; }
        public int HomeworkNumber { get; set; }
        public string HomeworkTitle { get; set; }
        public int MaxPoints { get; set; }
    }
}