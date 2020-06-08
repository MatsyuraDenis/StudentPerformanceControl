using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.Models.Dtos;

namespace BusinessLogic.Services
{
    public interface IGroupService
    {
        Task<IList<GroupDto>> GetGroupsAsync();
        Task<GroupDto> GetGroupAsync(int groupId);
    }
}