namespace Entity.Models.Dtos.Subject
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public int SubjectInfoId { get; set; }
        public int? SubjectSettingId { get; set; }
        public int GroupId { get; set; }
        public int Module1MaxPoints { get; set; }
        public int Module2MaxPoints { get; set; }
        public int ExamMaxPoints { get; set; }
        public int NumberOfHomeworks { get; set; }
        public int MaxPoints { get; set; }

        public string GroupName { get; set; }
        public string SubjectName { get; set; }
    }
}