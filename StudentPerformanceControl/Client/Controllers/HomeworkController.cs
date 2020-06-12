using System;
using System.Threading.Tasks;
using BusinessLogic.Services;
using DataCore.Exceptions;
using Entity.Models.Dtos;
using Entity.Models.Dtos.Homeworks;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class HomeworkController : Controller
    {
        #region Dependencies

        private readonly IHomeworkService _homeworkService;

        #endregion

        #region ctor

        public HomeworkController(IHomeworkService homeworkService)
        {
            _homeworkService = homeworkService;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> Index(int subjectId)
        {
            try
            {
                var homeworks = await _homeworkService.GetHomeworksAsync(subjectId);
                return View(homeworks);
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

        public async Task<ActionResult> Create(int subjectId, int groupId)
        {
            var homework = new NewHomeworkDto
            {
                SubjectId = subjectId,
                GroupId = groupId,
                DataDto = await _homeworkService.GetCreateHomeworkDataAsync(subjectId)
            };
            return View(homework);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NewHomeworkDto homeworkDto)
        {
            await _homeworkService.CreateHomeworkAsync(homeworkDto);
            return RedirectToAction("Edit", "Group", new {groupId = homeworkDto.GroupId});
        }

        public async Task<ActionResult> Edit(int homeworkId)
        {
            var homework = await _homeworkService.GetHomeworkDtoAsync(homeworkId);
            return View(homework);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditHomework(HomeworkDto homeworkDto)
        {
            try
            {
                await _homeworkService.EditHomeworkAsync(homeworkDto);
                return RedirectToAction("Index", "Homework", new {subjectId = homeworkDto.SubjectId} );
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
        
        public async Task<ActionResult> Delete(int homeworkId, int subjectId)
        {
            try
            {
                await _homeworkService.DeleteHomeworkAsync(homeworkId);
                return RedirectToAction("Index", "Homework", new {subjectId = subjectId} );
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