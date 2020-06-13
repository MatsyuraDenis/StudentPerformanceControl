namespace Entity.Models.Dtos.StudentPerformance
{
    public class StudentHomeworkPerformanceDto
    {
        public int HomeworkId { get; set; }
        public int HomeworkResultId { get; set; }
        public int HomeworkNumber { get; set; }
        public int HomeworkTitle { get; set; }
        public int Points { get; set; }
        public int MaxPoints { get; set; }
    }
}