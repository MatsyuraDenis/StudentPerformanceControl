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

        public ActionResult Create(int subjectSettingId)
        {
            var homework = new NewHomeworkDto
            {
                SubjectSettingsId = subjectSettingId
            };
            return View(homework);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NewHomeworkDto homeworkDto, int subjectId)
        {
            await _homeworkService.CreateHomeworkAsync(homeworkDto);
            return RedirectToAction("Edit", "Subject", new {subjectId =  subjectId});
        }

        public ActionResult Edit(HomeworkDto homeworkDto)
        {
            return View(homeworkDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditHomework(HomeworkDto homeworkDto)
        {
            try
            {
                await _homeworkService.EditHomeworkAsync(homeworkDto);
                return RedirectToAction("Details", "Subject", new {id = homeworkDto.SubjectId} );
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
        
        public async Task<ActionResult> Delete(HomeworkDto homeworkDto)
        {
            try
            {
                await _homeworkService.DeleteHomeworkAsync(homeworkDto.HomeworkId);
                return RedirectToAction("Details", "Subject", new {id = homeworkDto.SubjectId} );
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