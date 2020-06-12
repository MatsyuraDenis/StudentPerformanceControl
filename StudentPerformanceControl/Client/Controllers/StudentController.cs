using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Services;
using DataCore.Exceptions;
using Entity.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        
        // GET: Student/Create
        public ActionResult Create(int groupId)
        {
            var student = new StudentDto
            {
                GroupId = groupId
            };
            return View(student);
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StudentDto studentDto)
        {
            try
            {
                await _studentService.AddStudentAsync(studentDto);

                return RedirectToAction("Edit", "Group", new {groupId = studentDto.GroupId});
            }
            catch (SPCException ex)
            {
                return View("ErrorView", new ErrorDto(ex.Message, ex.StatusCode));
            }
            catch
            {
                return View("Error");
            }
        }
        
        // GET: Student/Delete/5
        public async Task<ActionResult> Delete(int id, int groupId)
        {
            try
            {
                await _studentService.RemoveStudentAsync(id);
                return RedirectToAction("Edit", "Group", new{ groupId = groupId});
            }
            catch (SPCException ex)
            {
                return View("ErrorView", new ErrorDto(ex.Message, ex.StatusCode));
            }
            catch (Exception ex)
            {
                return View("ErrorView", new ErrorDto(ex.Message, 500));
            }
        }
    }
}