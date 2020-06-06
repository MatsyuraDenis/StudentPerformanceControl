namespace Entity.Models.Dtos
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public int TypeId { get; set; }

        public string Teacher { get; set; }
        public string Name { get; set; }
    }
}