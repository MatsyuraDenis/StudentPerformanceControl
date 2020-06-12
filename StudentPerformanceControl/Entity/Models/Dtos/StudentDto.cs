using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entity.Models.Dtos.PerformanceInfos;

namespace Entity.Models.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 2)]
        public string SecondName { get; set; }
        public int GroupId { get; set; }
        public int GroupName { get; set; }
    }
}