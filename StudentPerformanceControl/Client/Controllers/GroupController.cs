using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Services;
using DataCore.EntityModels;
using DataCore.Exceptions;
using Entity.Models.Dtos;
using Entity.Models.Dtos.Group;
using Entity.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Client.Controllers
{
    public class GroupController : Controller
    {
        #region MyRegion

        private readonly IGroupService _groupService;
        private readonly ISubjectInfoService _subjectInfoService;

        #endregion

        #region ctor

        public GroupController(IGroupService groupService, ISubjectInfoService subjectInfoService)
        {
            _groupService = groupService;
            _subjectInfoService = subjectInfoService;
        }

        #endregion

        #region Methods
        
        // GET: Group
        public async Task<ActionResult> Index(GroupTypes groupType = GroupTypes.Active)
        {
            var groups = await _groupService.GetGroupsAsync((int)groupType);
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
            ViewBag.Subjects = await _subjectInfoService.GetSubjectInfosAsync();
            return View();
        }
        
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Save(int groupId)
        {
            await _groupService.SaveAsync(groupId);

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public async Task<ActionResult> Update(int groupId)
        {
            var newId = await _groupService.BoostGroupAsync(groupId);
            var group = await _groupService.GetGroupAsync(newId);

            return RedirectToAction("Edit", group);
        }

        // POST: Group/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddGroupDto groupDto)
        {
            try
            {
                var newGroupId = await _groupService.AddGroupAsync(groupDto);
                var newGroup = await _groupService.GetGroupAsync(newGroupId);

                return RedirectToAction("Edit", new { groupId = newGroup.Id } );
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
        
        public async Task<ActionResult> Edit(int groupId)
        {
            var group = await _groupService.GetGroupAsync(groupId);
            return View(group);
        }

        public async Task<ActionResult> Deactivate(int id)
        {
            try
            {
                await _groupService.DeactivateGroupAsync(id);
                return RedirectToAction(nameof(Index));
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

        public async Task<ActionResult> Delete(int groupId)
        {
            try
            {
                await _groupService.DeleteGroupAsync(groupId);
                return RedirectToAction("Index", new {groupType = GroupTypes.Created} );
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
        
        #endregion
    }
}