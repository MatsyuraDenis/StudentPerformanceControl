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

        #endregion

        #region ctor

        public SubjectController(ISubjectService subjectService, IPerformanceService performanceService)
        {
            _subjectService = subjectService;
            _performanceService = performanceService;
        }

        #endregion

        #region Methods

        // GET: Subject
        public async Task<ActionResult> Index()
        {
            var subjects = await _subjectService.GetSubjectInfosAsync();
            return View(subjects);
        }

        // GET: Subject/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var subjects = await _subjectService.GetSubjectPerformanceInfoAsync(id);
            return View(subjects);
        }

        // GET: Subject/Create
        public async Task<ActionResult> Create(int groupId)
        {
            var subject = new NewSubjectDto
            {
                GroupId = groupId
            };

            var subjects = await _subjectService.GetSubjectInfosAsync(groupId);
            ViewBag.Subjects = new SelectList(subjects, "Id", "Title");
            return View(subject);
        }

        // POST: Subject/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NewSubjectDto subject)
        {
            try
            {
                await _subjectService.CreateSubjectAsync(subject);

                return RedirectToAction("Edit", "Group", new {groupId = subject.GroupId});
            }
            catch(Exception ex)
            {
                return View("ErrorView", new ErrorDto(ex.Message, 400));
            }
        }

        // GET: Subject/Edit/5
        public async Task<ActionResult> Edit(int studentId, int subjectId)
        {
            var performance = await _performanceService.GetStudentPerformanceAsync(studentId, subjectId);
            return View(performance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSubject(StudentPerformanceDto studentPerformance)
        {
            try
            {
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
                return RedirectToAction("Details", "Group", new { id = groupId });
            }
            catch (Exception ex)
            {
                return View("ErrorView", new ErrorDto(ex.Message, 400));
            }
        }
        
        #endregion

    }
}