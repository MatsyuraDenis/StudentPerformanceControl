using System.Threading.Tasks;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace StudentPerformanceControl.Controllers
{
    [Route("groups")]
    public class GroupController : ControllerBase
    {
        #region Dependencies

        private readonly IGroupService _groupService;
        
        #endregion

        #region ctor

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        #endregion

        #region Methods

        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await _groupService.GetGroupsAsync();
            return Ok(groups);
        }

        #endregion
    }
}