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
                if (!ModelState.IsValid)
                {
                    return View(subjectDto);
                }
                await _subjectInfoService.CreateSubjectInfoAsync(subjectDto);
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

        public async Task<ActionResult> Edit(int subjectInfoId)
        {
            try
            {
                var subject = await _subjectInfoService.GetSubjectInfoAsync(subjectInfoId);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SubjectInfoDto subjectDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(subjectDto);
                }
                await _subjectInfoService.EditSubjectInfoAsync(subjectDto);
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