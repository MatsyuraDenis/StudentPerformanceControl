using System;
using System.Threading.Tasks;
using BusinessLogic.Services;
using DataCore.Exceptions;
using Entity.Models.Dtos;
using Entity.Models.Dtos.Subject;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class SubjectInfoController : Controller
    {
        #region Dependencies
        
        private readonly ISubjectInfoService _subjectInfoService;

        #endregion

        #region ctor

        public SubjectInfoController(ISubjectInfoService subjectInfoService)
        {
            _subjectInfoService = subjectInfoService;
        }

        #endregion

        #region Methods

        public async Task<ActionResult> Index()
        {
            var subjects = await _subjectInfoService.GetSubjectInfosAsync();
            return View(subjects);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SubjectInfoDto subjectDto)
        {
            try
            {
                await _subjectInfoService.CreateSubjectInfoAsync(subjectDto);
                return RedirectToAction("Index");
            }
            catch (SPCException ex)
            {
                return View("ErrorView", new ErrorDto(ex.Message, ex.StatusCode));
            }
        }

        public ActionResult Edit(SubjectInfoDto subjectInfoDto)
        {
            return View(subjectInfoDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSubjectInfo(SubjectInfoDto subjectDto)
        {
            try
            {
                await _subjectInfoService.EditSubjectInfoAsync(subjectDto);
                return RedirectToAction("Index");
            }
            catch (SPCException ex)
            {
                return View("ErrorView", new ErrorDto(ex.Message, ex.StatusCode));
            }
        }

        public async Task<ActionResult> Delete(int subjectInfoId)
        {
            try
            {
                await _subjectInfoService.DeleteSubjectInfoAsync(subjectInfoId);
                return RedirectToAction("Index");
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