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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subject/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewSubjectDto subject)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
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
        public ActionResult Delete(int id)
        {
            return RedirectToAction("DeleteGroup", new {groupId = id});
        }

        // POST: Subject/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteGroup(int groupId)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}