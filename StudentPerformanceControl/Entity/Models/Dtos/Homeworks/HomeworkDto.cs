namespace Entity.Models.Dtos.Homeworks
{
    public class HomeworkDto
    {
        public int HomeworkId { get; set; }
        public int SubjectId { get; set; }
        public string HomeworkTitle { get; set; }
        public int Number { get; set; }
        public int MaxPoints { get; set; }
    }
}