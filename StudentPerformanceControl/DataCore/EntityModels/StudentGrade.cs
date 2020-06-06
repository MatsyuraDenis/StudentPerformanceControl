namespace DataCore.EntityModels
{
    public class StudentGrade
    {
        public int StudentGradeId { get; set; }
        public int StudentId { get; set; }
        public int? TestId { get; set; }
        public int? LaboratoryId { get; set; }
        public int? ExamId { get; set; }

        public Test Test { get; set; }
        public Laboratory Laboratory { get; set; }
        public Exam Exam { get; set; }
    }
}