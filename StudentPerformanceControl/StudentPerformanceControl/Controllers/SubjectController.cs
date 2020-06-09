using System.Threading.Tasks;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace StudentPerformanceControl.Controllers
{
    [Route("subject")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        
        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet("performance")]
        public async Task<IActionResult> GetSubjectPerformanceAsync([FromQuery] int subjectId)
        {
            var subjectPerformance = await _subjectService.GetSubjectPerformanceInfoAsync(subjectId);
            return Ok(subjectPerformance);
        }
    }
}