using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Services;
using DataCore.Exceptions;
using Entity.Models.Dtos;
using Entity.Models.Dtos.Group;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Client.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly ISubjectService _subjectService;

        public GroupController(IGroupService groupService, ISubjectService subjectService)
        {
            _groupService = groupService;
            _subjectService = subjectService;
        }

        // GET: Group
        public async Task<ActionResult> Index()
        {
            var groups = await _groupService.GetGroupsAsync();
            return View(groups);
        }

        // GET: Group/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var group = await _groupService.GetGroupAsync(id);
            return View(group);
        }

        // GET: Group/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Subjects = await _subjectService.GetSubjectInfosAsync();
            return View();
        }

        // POST: Group/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddGroupDto groupDto)
        {
            try
            {
                var newGroupId = await _groupService.AddGroup(groupDto);

                return RedirectToAction("Details", new { id = newGroupId } );
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

        // GET: Group/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Group/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}