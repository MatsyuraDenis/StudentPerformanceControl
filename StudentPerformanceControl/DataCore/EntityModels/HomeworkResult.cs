namespace DataCore.EntityModels
{
    public class HomeworkResult
    {
        public int HomeworkResultId { get; set; }
        public int HomeworkInfoId { get; set; }
        public int StudentPerformanceId { get; set; }
        public int Points { get; set; }

        public StudentPerformance StudentPerformance { get; set; }
    }
}