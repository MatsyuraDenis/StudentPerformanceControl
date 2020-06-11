using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Services;
using DataCore.Exceptions;
using Entity.Models.Dtos;
using Entity.Models.Dtos.Subject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

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
        public ActionResult Create(int groupId)
        {
            var subject = new NewSubjectDto
            {
                GroupId = groupId
            };
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
            catch
            {
                return View();
            }
        }

        // GET: Subject/Edit/5
        public ActionResult Edit(int id)
        {
            return RedirectToAction("Edit", new {groupId = id});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSubject(int groupId)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
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
    }
}