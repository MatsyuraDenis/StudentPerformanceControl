using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.EntityModels;
using DataCore.Exceptions;
using DataCore.Factories;
using DataCore.Repository;
using Entity.Models.Dtos.Homeworks;
using Entity.Models.Dtos.PerformanceInfos;
using Entity.Models.Dtos.Subject;
using Entity.Models.Enums;
using Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services.Impl
{
    public class SubjectService : ISubjectService
    {
        #region Dependencies

        private readonly IRepository _repository;
        private readonly ILogService _logService;

        #endregion

        #region ctor

        public SubjectService(IRepositoryFactory repositoryFactory, ILogService logService)
        {
            _logService = logService;
            _repository = repositoryFactory.GetMsSqlRepository();
        }

        #endregion

        #region Implementation
        
        public async Task<SubjectPerformanceInfoDto> GetSubjectPerformanceInfoAsync(int subjectId)
        {
            _logService.LogInfo($"Start loading subject performance info with subject id = {subjectId}");

            
            var subjectRes = await _repository.GetAll<SubjectSetting>()
                .Where(setting => setting.SubjectId == subjectId)
                .Select(setting => new SubjectPerformanceInfoDto
                {
                    SubjectId = setting.SubjectId,
                    GroupType = setting.Subject.Group.GroupTypeId,
                    SubjectSettings = new SubjectSettingDto
                    {
                        SubjectSettingId = setting.SubjectSettingId,
                        Module1MaxPoints = setting.Module1TestMaxPoints,
                        Module2MaxPoints = setting.Module2TestMaxPoints,
                        ExamMaxPoint = setting.ExamMaxPoints,
                        HomeworkInfos = setting.HomeworkInfos.Select(info => new HomeworkDto
                        {
                            HomeworkId = info.HomeworkInfoId,
                            MaxPoints = info.MaxPoints,
                            SubjectId = setting.SubjectId,
                            Number = info.Number,
                            HomeworkTitle = info.Title
                        })
                    },
                    StudentPerformances = setting.Subject.StudentPerformances.Select(performance => new StudentPerformanceDto
                    {
                        StudentId = performance.StudentId,
                        SubjectId = performance.SubjectId,
                        StudentName = performance.Student.Name,
                        StudentSecondName = performance.Student.SecondName,
                        ExamResult = performance.ExamPoints,
                        Module1Result = performance.Module1TestPoints,
                        Module2Result = performance.Module2TestPoints,
                        Homeworks = performance.HomeworkResults.Select(result => new HomeworkResultDto
                        {
                            HomeworkId = result.HomeworkInfoId,
                            HomeworkResultId = result.HomeworkResultId,
                            Points = result.Points
                        })
                    })
                })
                .SingleOrDefaultAsync()
                ?? throw new SPCException($"subject with id {subjectId} does not exists", StatusCodes.Status404NotFound);
            
            

            foreach (var studentPerformance in subjectRes.StudentPerformances)
            {
                studentPerformance.TotalPoints = studentPerformance.Homeworks
                                                     .Sum(homework => (int) homework.Points)
                                                    + studentPerformance.Module1Result
                                                    + studentPerformance.Module2Result 
                                                    + studentPerformance.ExamResult;

                studentPerformance.EditableHomeworks = studentPerformance.Homeworks.ToList();
            }

            _logService.LogInfo($"Loading subject performance info with subject id = {subjectId} completed");

            return subjectRes;
        }

        public async Task<SubjectDto> GetSubjectAsync(int subjectId)
        {
            var res = await _repository.GetAll<SubjectSetting>()
                       .Where(setting => setting.SubjectId == subjectId)
                       .Select(setting => new SubjectDto
                       {
                           Id = setting.SubjectId,
                           SubjectSettingId = setting.SubjectSettingId,
                           GroupId = setting.Subject.GroupId,
                           SubjectInfoId = setting.Subject.SubjectInfoId,
                           ExamMaxPoints = setting.ExamMaxPoints,
                           Module1MaxPoints = setting.Module1TestMaxPoints,
                           Module2MaxPoints = setting.Module2TestMaxPoints,
                           NumberOfHomeworks = setting.HomeworkInfos.Count(),
                           MaxPoints = setting.ExamMaxPoints +
                                       setting.Module1TestMaxPoints +
                                       setting.Module2TestMaxPoints +
                                       setting.HomeworkInfos.Sum(homework => homework.MaxPoints)
                       })
                       .SingleOrDefaultAsync()
                   ?? throw new SPCException($"Subject with id {subjectId} does not exists in database", 404);

            return res;
        }

        public async Task CreateSubjectAsync(SubjectDto subjectDto)
        {
            var subject = new Subject
            {
                GroupId = subjectDto.GroupId,
                SubjectInfoId = subjectDto.SubjectInfoId
            };


            _repository.Add(subject);

            await _repository.SaveContextAsync();
            
            var subjectSetting = new SubjectSetting
            {
                Module1TestMaxPoints = subjectDto.Module1MaxPoints,
                Module2TestMaxPoints = subjectDto.Module2MaxPoints,
                ExamMaxPoints = subjectDto.ExamMaxPoints,
                SubjectId = subject.SubjectId
            };
            
            _repository.Add(subjectSetting);

            await _repository.SaveContextAsync();

            var studentIds = await _repository.GetAll<Student>()
                .Where(student => student.GroupId == subjectDto.GroupId)
                .Select(student => student.StudentId)
                .ToListAsync();

            foreach (var studentId in studentIds)
            {
                _repository.Add(new StudentPerformance
                {
                    Module1TestPoints = 0,
                    Module2TestPoints = 0,
                    ExamPoints = 0,
                    SubjectId = subject.SubjectId,
                    StudentId = studentId
                });
            }

            await _repository.SaveContextAsync();
        }

        public async Task EditSubjectAsync(SubjectDto subjectDto)
        {
            var dbSubject = await _repository.GetAll<SubjectSetting>()
                .SingleOrDefaultAsync(subject => subject.SubjectId == subjectDto.Id);
            
            dbSubject.Module1TestMaxPoints = subjectDto.Module1MaxPoints;
            dbSubject.Module2TestMaxPoints = subjectDto.Module2MaxPoints;
            dbSubject.ExamMaxPoints = subjectDto.ExamMaxPoints;

            _repository.Update(dbSubject);
            
            await _repository.SaveContextAsync();
        }

        
        public async Task RemoveSubjectAsync(int subjectId)
        {
            var dbSubject = await _repository.GetAll<Subject>()
                .SingleOrDefaultAsync(subject => subject.SubjectId == subjectId);

            _repository.Delete(dbSubject);

            await _repository.SaveContextAsync();

        }

        #endregion
    }
}