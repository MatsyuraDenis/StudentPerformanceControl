using System.Threading.Tasks;
using BusinessLogic.Services;
using Entity.Models.Dtos.Homeworks;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers.API
{
    [Route("api/homework")]
    public class HomeworkController : ErrorController
    {
        #region Dependencies

        private readonly IHomeworkService _homeworkService;

        #endregion

        #region ctor

        public HomeworkController(IHomeworkService homeworkService)
        {
            _homeworkService = homeworkService;
        }

        #endregion

        #region Methods
        
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery]int subjectId)
        {
            return await HandleRequestAsync(async () => await _homeworkService.GetHomeworksAsync(subjectId));
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]NewHomeworkDto homeworkDto)
        {
            return await HandleRequestAsync(async () => await _homeworkService.CreateHomeworkAsync(homeworkDto));
        }
        
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody]HomeworkDto homeworkDto)
        {
            return await HandleRequestAsync(async () =>  await _homeworkService.EditHomeworkAsync(homeworkDto));
        }
        
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody]int homeworkId, int subjectId)
        {
            return await HandleRequestAsync(async () => await _homeworkService.DeleteHomeworkAsync(homeworkId));

        }
        
        #endregion
    }
}