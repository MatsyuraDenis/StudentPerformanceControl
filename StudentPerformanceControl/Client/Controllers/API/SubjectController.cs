using System.Threading.Tasks;
using BusinessLogic.Services;
using Entity.Models.Dtos.PerformanceInfos;
using Entity.Models.Dtos.Subject;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers.API
{
    [Route("api/subject")]
    public class ApiSubjectController : ErrorController
    {
        #region MyRegion
                  
        private readonly ISubjectService _subjectService;
        private readonly IPerformanceService _performanceService;
        private readonly ISubjectInfoService _subjectInfoService;
                  
        #endregion
                  
        #region ctor
                  
        public ApiSubjectController(ISubjectService subjectService, IPerformanceService performanceService, ISubjectInfoService subjectInfoService)
        { 
            _subjectService = subjectService;
            _performanceService = performanceService;
            _subjectInfoService = subjectInfoService;
        }
                  
        #endregion
                  
        #region Methods
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return await HandleRequestAsync(async () => await _subjectInfoService.GetSubjectInfosAsync());
        }
        
        [HttpGet]
        public async Task<IActionResult> Details([FromQuery]int id)
        {
            return await HandleRequestAsync(async () => await _subjectService.GetSubjectPerformanceInfoAsync(id));
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]SubjectDto subject)
        {
            return await HandleRequestAsync(async () => await _subjectService.CreateSubjectAsync(subject));
        }
        
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody]SubjectDto subjectDto)
        {
            return await HandleRequestAsync(async () => await _subjectService.EditSubjectAsync(subjectDto));
        }
        
        [HttpPut("performance")]
        public async Task<IActionResult> EditSubjectPerformance([FromBody]StudentPerformanceDto studentPerformance)
        {
            return await HandleRequestAsync(async () =>
                await _performanceService.EditPerformanceAsync(studentPerformance));
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int id)
        {
            return await HandleRequestAsync(async () => await _subjectService.RemoveSubjectAsync(id));
        }
        #endregion
    }
}