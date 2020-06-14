using System;
using System.Threading.Tasks;
using BusinessLogic.Services;
using DataCore.Exceptions;
using Entity.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers.API
{
    [Microsoft.AspNetCore.Components.Route("api/student")]
    public class StudentController : ErrorController
    {
        #region Dependencies

        private readonly IStudentService _studentService;
        private readonly IPerformanceService _performanceService;

        #endregion

        #region ctor

        public StudentController(IStudentService studentService, IPerformanceService performanceService)
        {
            _studentService = studentService;
            _performanceService = performanceService;
        }

        #endregion

        #region Methods
        
        [HttpGet("details")]
        public async Task<IActionResult> Details([FromQuery]int studentId)
        {
            return await HandleRequestAsync(async () =>
                await _performanceService.GetStudentPerformanceAsync(studentId));
        }
        

        // POST: Student/Create
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]StudentDto studentDto)
        {
            return await HandleRequestAsync(async () => await _studentService.AddStudentAsync(studentDto));
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody]StudentDto studentDto)
        {
            return await HandleRequestAsync(async () => await _studentService.EditStudentAsync(studentDto));
        }
        
        
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery]int id)
        {
            return await HandleRequestAsync(async () => await _studentService.RemoveStudentAsync(id));
        }
        
        #endregion
    }
}