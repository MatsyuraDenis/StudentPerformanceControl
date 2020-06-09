namespace DataCore.EntityModels
{
    public class HomeworkInfo
    {
        public int HomeworkInfoId { get; set; }
        public int SubjectSettingId { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public int MaxPoints { get; set; }
        
        public SubjectSetting SubjectSetting { get; set; }
    }
}