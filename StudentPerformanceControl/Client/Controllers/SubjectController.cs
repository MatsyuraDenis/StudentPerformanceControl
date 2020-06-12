using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Services;
using DataCore.EntityModels;
using DataCore.Exceptions;
using Entity.Models.Dtos;
using Entity.Models.Dtos.PerformanceInfos;
using Entity.Models.Dtos.Subject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Client.Controllers
{
    public class SubjectController : Controller
    {
        #region MyRegion

        private readonly ISubjectService _subjectService;
        private readonly IPerformanceService _performanceService;
        private readonly ISubjectInfoService _subjectInfoService;

        #endregion

        #region ctor

        public SubjectController(ISubjectService subjectService, IPerformanceService performanceService, ISubjectInfoService subjectInfoService)
        {
            _subjectService = subjectService;
            _performanceService = performanceService;
            _subjectInfoService = subjectInfoService;
        }

        #endregion

        #region Methods

        // GET: Subject
        public async Task<ActionResult> Index()
        {
            try
            {
                var subjects = await _subjectInfoService.GetSubjectInfosAsync();
                return View(subjects);
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

        // GET: Subject/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var subjects = await _subjectService.GetSubjectPerformanceInfoAsync(id);
                return View(subjects);
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

        // GET: Subject/Create
        public async Task<ActionResult> Create(int groupId)
        {
            try
            {
                var subject = new SubjectDto
                {
                    GroupId = groupId
                };

                var subjects = await _subjectInfoService.GetSubjectInfosAsync(groupId);
                ViewBag.Subjects = new SelectList(subjects, "Id", "Title");
                return View(subject);
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

        // POST: Subject/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SubjectDto subject)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(subject);
                }
                await _subjectService.CreateSubjectAsync(subject);

                return RedirectToAction("Edit", "Group", new {groupId = subject.GroupId});
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

        // GET: Subject/Edit/5
        public async Task<ActionResult> Edit(int studentId, int subjectId)
        {
            try
            {
                var performance = await _performanceService.GetStudentPerformanceAsync(studentId, subjectId);
                return View(performance);
            }
            catch(SPCException ex)
            {
                return View("ErrorView", new ErrorDto(ex.Message, ex.StatusCode));
            }
            catch(Exception ex)
            {
                return View("Error");
            }
        }

        public async Task<ActionResult> EditSubject(int subjectId)
        {
            try
            {
                var subject = await _subjectService.GetSubjectAsync(subjectId);
                
                return View(subject);
            }
            catch(SPCException ex)
            {
                return View("ErrorView", new ErrorDto(ex.Message, ex.StatusCode));
            }
            catch(Exception ex)
            {
                return View("Error");
            }
        }
        
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> EditSubject(SubjectTestDto subjectDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(subjectDto);
                }
                await _subjectService.EditSubjectAsync(subjectDto.Subject);
                
                return RedirectToAction("Edit", "Group", new {groupId = subjectDto.Subject.GroupId});
            }
            catch(SPCException ex)
            {
                return View("ErrorView", new ErrorDto(ex.Message, ex.StatusCode));
            }
            catch(Exception ex)
            {
                return View("ErrorView", new ErrorDto(ex.Message, 500));
            }
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSubjectPerformance(StudentPerformanceDto studentPerformance)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Edit", studentPerformance);
                }
                await _performanceService.EditPerformanceAsync(studentPerformance);
                
                return RedirectToAction("Details", "Subject", new {id = studentPerformance.SubjectId});
            }
            catch(SPCException ex)
            {
                return View("ErrorView", new ErrorDto(ex.Message, ex.StatusCode));
            }
            catch(Exception ex)
            {
                return View("Error");
            }
        }

        // GET: Subject/Delete/5
        public async Task<ActionResult> Delete(int id, int groupId)
        {
            try
            {
                await _subjectService.RemoveSubjectAsync(id);
                return RedirectToAction("Edit", "Group", new { id = groupId });
            }
            catch(SPCException ex)
            {
                return View("ErrorView", new ErrorDto(ex.Message, ex.StatusCode));
            }
            catch(Exception ex)
            {
                return View("Error");
            }
        }
        
        #endregion

    }
}