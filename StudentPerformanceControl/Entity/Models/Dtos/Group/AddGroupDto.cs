using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity.Models.Dtos.Group
{
    public class AddGroupDto
    {
        public int GroupId { get; set; }
        [MaxLength(32)]
        public string GroupName { get; set; }
    }
}