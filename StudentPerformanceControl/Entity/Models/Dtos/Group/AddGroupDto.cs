using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity.Models.Dtos.Group
{
    public class AddGroupDto
    {
        public int GroupId { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 2)]
        public string GroupName { get; set; }
    }
}