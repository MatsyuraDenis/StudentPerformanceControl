namespace DataCore.EntityModels
{
    public class TeacherSubjectInfo
    {
        public int TeacherSubjectInfoId { get; set; }
        public int TeacherId { get; set; }
        public int SubjectInfoId { get; set; }

        public Teacher Teacher { get; set; }
        public SubjectInfo SubjectInfo { get; set; }
    }
}