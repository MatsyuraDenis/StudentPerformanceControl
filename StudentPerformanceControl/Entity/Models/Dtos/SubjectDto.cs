namespace Entity.Models.Dtos
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }

        public string SubjectName { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSecondName { get; set; }
    }
}