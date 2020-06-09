using System.Threading.Tasks;
using BusinessLogic.Services;
using DataCore.Factories;
using DataCore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace StudentPerformanceControl.Controllers
{
    [Route("student")]
    public class StudentController : ControllerBase
    {
        #region Dependencies

        private readonly IStudentService _studentService;

        #endregion

        #region ctor

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        #endregion

        #region Methods

        [HttpGet("total/performance")]
        public async Task<IActionResult> GetStudentPerformance([FromQuery]int studentId)
        {
            var studentPerformance = await _studentService.GetStudentPerformanceAsync(studentId);
            return Ok(studentPerformance);
        }

        #endregion
    }
}