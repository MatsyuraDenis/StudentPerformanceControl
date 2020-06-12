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
                if (!ModelState.IsValid)
                {
                    return View(studentDto);
                }
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

        public async Task<ActionResult> Edit(int studentId)
        {
            try
            {
                var student = await _studentService.GetStudentAsync(studentId);
                return View(student);
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
        
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Edit(StudentDto studentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(studentDto);
                }
                await _studentService.EditStudentAsync(studentDto);
                return RedirectToAction("Edit", "Group", new {groupId = studentDto.GroupId});
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

        #endregion
       
    }
}