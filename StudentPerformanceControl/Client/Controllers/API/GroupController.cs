using System.Threading.Tasks;
using BusinessLogic.Services;
using Entity.Models.Dtos.Group;
using Entity.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers.API
{
    [Route("api/group")]
    public class GroupController : ErrorController
    {
        #region Dependencies

        private readonly IGroupService _groupService;
        private readonly ISubjectInfoService _subjectInfoService;

        #endregion

        #region ctor

        public GroupController(IGroupService groupService, ISubjectInfoService subjectInfoService)
        {
            _groupService = groupService;
            _subjectInfoService = subjectInfoService;
        }

        #endregion

        #region Methods
        
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] GroupTypes groupType = GroupTypes.Active)
        {
            return await HandleRequestAsync(async () => await _groupService.GetGroupsAsync((int) groupType));
        }
        
        [HttpGet("id")]
        public async Task<IActionResult> Details([FromQuery]int id)
        {
            return await HandleRequestAsync(async () => await _groupService.GetGroupAsync(id));
        }
        
        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            return await HandleRequestAsync(async () => await _subjectInfoService.GetSubjectInfosAsync());
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]AddGroupDto groupDto)
        {
            return await HandleRequestAsync(async () => await _groupService.AddGroupAsync(groupDto));
        }
        
        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody]int groupId)
        {
            return await HandleRequestAsync(async () =>  await _groupService.SaveAsync(groupId));
        }
        
        [HttpGet("update")]
        public async Task<IActionResult> Update([FromQuery]int groupId)
        {
            var newId = await _groupService.BoostGroupAsync(groupId);

            return await HandleRequestAsync(async () => await _groupService.GetGroupAsync(newId));
        }
        
        [HttpPost("edit/name")]
        public async Task<IActionResult> ChangeGroupName([FromQuery]AddGroupDto groupDto)
        {
            return await HandleRequestAsync(async () => await _groupService.ChangeGroupNameAsync(groupDto));
        }
        
        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromQuery] int groupId)
        {
            return await HandleRequestAsync(async () => await _groupService.GetGroupAsync(groupId));
        }
        
        [HttpPost("deactivate")]
        public async Task<IActionResult> Deactivate([FromQuery]int id)
        {
            return await HandleRequestAsync(async () => await _groupService.DeactivateGroupAsync(id));
        }
        
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int groupId)
        {
            return await HandleRequestAsync(async () => await _groupService.DeleteGroupAsync(groupId));
        }
        
        #endregion
    }
}