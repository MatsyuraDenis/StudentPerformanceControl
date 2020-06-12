using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.Models.Dtos;
using Entity.Models.Dtos.Group;

namespace BusinessLogic.Services
{
    public interface IGroupService
    {
        Task<IList<GroupDto>> GetGroupsAsync(int groupType);
        Task<GroupDto> GetGroupAsync(int groupId);
        Task<int> AddGroupAsync(AddGroupDto group);
        Task SaveAsync(int groupId);
        Task DeactivateGroupAsync(int groupId);
    }
}