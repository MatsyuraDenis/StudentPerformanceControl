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

            var dbSubject = await _repository.GetAll<Subject>()
                .Where(subject => subject.SubjectId == subjectId)
                .Select(subject => new SubjectPerformanceInfoDto
                {
                    SubjectId = subject.SubjectId,
                    GroupType = subject.Group.GroupTypeId,
                    SubjectSettings = new SubjectSettingDto
                    {
                        SubjectSettingId = subject.SubjectSetting.SubjectSettingId,
                        Module1MaxPoints = subject.SubjectSetting.Module1TestMaxPoints,
                        Module2MaxPoints = subject.SubjectSetting.Module2TestMaxPoints,
                        ExamMaxPoint = subject.SubjectSetting.ExamMaxPoints,
                        HomeworkInfos = subject.SubjectSetting.HomeworkInfos.Select(info => new HomeworkDto
                        {
                            HomeworkId = info.HomeworkInfoId,
                            MaxPoints = info.MaxPoints,
                            SubjectId = subject.SubjectId,
                            Number = info.Number,
                            HomeworkTitle = info.Title
                        })
                    },
                    StudentPerformances = subject.StudentPerformances.Select(performance => new StudentPerformanceDto
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

            foreach (var studentPerformance in dbSubject.StudentPerformances)
            {
                studentPerformance.TotalPoints = studentPerformance.Homeworks
                                                     .Sum(homework => (int) homework.Points)
                                                    + studentPerformance.Module1Result
                                                    + studentPerformance.Module2Result 
                                                    + studentPerformance.ExamResult;

                studentPerformance.EditableHomeworks = studentPerformance.Homeworks.ToList();
            }

            _logService.LogInfo($"Loading subject performance info with subject id = {subjectId} completed");

            return dbSubject;
        }

        public async Task<SubjectDto> GetSubjectAsync(int subjectId)
        {
            return await _repository.GetAll<Subject>()
                       .Where(subject => subject.SubjectId == subjectId)
                       .Select(subject => new SubjectDto
                       {
                           Id = subject.SubjectId,
                           SubjectSettingId = subject.SubjectSettingId,
                           GroupId = subject.GroupId,
                           SubjectInfoId = subject.SubjectInfoId,
                           ExamMaxPoints = subject.SubjectSetting.ExamMaxPoints,
                           Module1MaxPoints = subject.SubjectSetting.Module1TestMaxPoints,
                           Module2MaxPoints = subject.SubjectSetting.Module2TestMaxPoints,
                           NumberOfHomeworks = subject.SubjectSetting.HomeworkInfos.Count(),
                           MaxPoints = subject.SubjectSetting.ExamMaxPoints +
                                       subject.SubjectSetting.Module1TestMaxPoints +
                                       subject.SubjectSetting.Module2TestMaxPoints +
                                       subject.SubjectSetting.HomeworkInfos.Sum(homework => homework.MaxPoints)
                       })
                       .SingleOrDefaultAsync()
                   ?? throw new SPCException($"Subject with id {subjectId} does not exists in database", 404);
        }

        public async Task CreateSubjectAsync(SubjectDto subjectDto)
        {
            var subject = new Subject
            {
                GroupId = subjectDto.GroupId,
                SubjectInfoId = subjectDto.SubjectInfoId,
                SubjectSetting = new SubjectSetting
                {
                    Module1TestMaxPoints = subjectDto.Module1MaxPoints,
                    Module2TestMaxPoints = subjectDto.Module2MaxPoints,
                    ExamMaxPoints = subjectDto.ExamMaxPoints,
                    SubjectId = subjectDto.Id
                }
            };
            
            _repository.Add(subject);

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

        public async Task DeleteSubjectInfoAsync(int subjectInfoId)
        {
            var subjectInfo = await _repository.GetAll<SubjectInfo>()
                                  .SingleOrDefaultAsync(info => info.SubjectInfoId == subjectInfoId)
                              ?? throw new SPCException($"Subject info with id {subjectInfoId} does bot exists", 404);
            
            _repository.Delete(subjectInfo);

            await _repository.SaveContextAsync();
        }

        public async Task RemoveSubjectAsync(int subjectId)
        {
            var dbSubject = await _repository.GetAll<Subject>()
                .SingleOrDefaultAsync(subject => subject.SubjectId == subjectId);

            _repository.Delete(dbSubject);

            await _repository.SaveContextAsync();

        }

        public async Task<IList<SubjectInfoDto>> GetSubjectInfosAsync()
        {
            return await _repository.GetAll<SubjectInfo>()
                .Select(info => new SubjectInfoDto
                {
                    Id = info.SubjectInfoId,
                    Title = info.Title,
                    GroupLearn = _repository.GetAll<Group>()
                        .Where(group => group.Subjects.Any(subject => subject.SubjectInfoId == info.SubjectInfoId)
                            && group.GroupTypeId == (int) GroupTypes.Active)
                        .Count(),
                    GroupLearned = _repository.GetAll<Group>()
                        .Where(group => group.Subjects.Any(subject => subject.SubjectInfoId == info.SubjectInfoId)
                                        && group.GroupTypeId == (int) GroupTypes.Former)
                        .Count()
                })
                .ToListAsync();
        }

        public async Task CreateSubjectInfoAsync(SubjectInfoDto subjectInfoDto)
        {
            _repository.Add(new SubjectInfo
            {
                Title = subjectInfoDto.Title
            });

            await _repository.SaveContextAsync();
        }

        public async Task EditSubjectInfoAsync(SubjectInfoDto subjectInfoDto)
        {
            var dbSubject = await _repository.GetAll<SubjectInfo>()
                                .Where(info => info.SubjectInfoId == subjectInfoDto.Id)
                                .SingleOrDefaultAsync()
                            ?? throw new SPCException($"Subject info with id {subjectInfoDto.Id} not exists", 404);

            dbSubject.Title = subjectInfoDto.Title;
            
            _repository.Update(dbSubject);

            await _repository.SaveContextAsync();
        }

        public async Task<IList<SubjectInfoDto>> GetSubjectInfosAsync(int groupId)
        {
            return await _repository.GetAll<SubjectInfo>()
                .Where(info => info.Subjects.All(subject => subject.GroupId != groupId))
                .Select(info => new SubjectInfoDto
                {
                    Id = info.SubjectInfoId,
                    Title = info.Title,
                    GroupLearn = _repository.GetAll<Group>()
                        .Where(group => group.Subjects.Any(subject => subject.SubjectInfoId == info.SubjectInfoId))
                        .Count()
                })
                .ToListAsync();
        }

        #endregion
    }
}