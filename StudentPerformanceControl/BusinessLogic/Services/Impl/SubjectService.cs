using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.EntityModels;
using DataCore.Exceptions;
using DataCore.Factories;
using DataCore.Repository;
using Entity.Models.Dtos;
using Entity.Models.Dtos.InfoDtos;
using Entity.Models.Dtos.PerformanceInfos;
using Entity.Models.Dtos.Subject;
using Entity.Models.Dtos.Teacher;
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
                    TeacherName = subject.Teacher.Name,
                    TeacherSecondName = subject.Teacher.SecondName,
                    SubjectSettings = new SubjectSettingDto
                    {
                        SubjectSettingId = subject.SubjectSetting.SubjectSettingId,
                        Module1MaxPoints = subject.SubjectSetting.Module1TestMaxPoints,
                        Module2MaxPoints = subject.SubjectSetting.Module2TestMaxPoints,
                        ExamMaxPoint = subject.SubjectSetting.ExamMaxPoints,
                        HomeworkSettings = subject.SubjectSetting.HomeworkInfos.Select(info => new HomeworkSettingDto
                        {
                            HomeworkId = info.HomeworkInfoId,
                            MaxPoints = info.MaxPoints,
                            HomeworkNumber = info.Number
                        })
                    },
                    StudentPerformances = subject.StudentPerformances.Select(performance => new StudentPerformanceDto
                    {
                        StudentId = performance.StudentId,
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
            
            _logService.LogInfo($"Loading subject performance info with subject id = {subjectId} completed");

            return dbSubject;
        }

        public async Task CreateSubjectAsync(NewSubjectDto subjectDto)
        {
            var subject = new Subject
            {
                GroupId = subjectDto.GroupId,
                TeacherId = subjectDto.TeacherId,
                SubjectInfoId = subjectDto.SubjectInfoId,
                SubjectSetting = new SubjectSetting
                {
                    Module1TestMaxPoints = subjectDto.Module1MaxPoints,
                    Module2TestMaxPoints = subjectDto.Module2MaxPoints,
                    ExamMaxPoints = subjectDto.ExamMaxPoints
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

        public async Task RemoveSubjectAsync(int subjectId)
        {
            var dbSubject = await _repository.GetAll<Subject>()
                .SingleOrDefaultAsync(subject => subject.SubjectId == subjectId);
            
            _repository.Delete(dbSubject);

            await _repository.SaveContextAsync();

        }

        public async Task<IList<SubjectInfoDetailsDto>> GetSubjectInfosAsync()
        {
            return await _repository.GetAll<SubjectInfo>()
                .Select(info => new SubjectInfoDetailsDto
                {
                    Id = info.SubjectInfoId,
                    Title = info.Title,
                    Teachers = info.TeacherSubjectInfos.Select(subjectInfo => new TeacherDto
                    {
                        Id = subjectInfo.Teacher.TeacherId,
                        Fullname = subjectInfo.Teacher.SecondName + " " + subjectInfo.Teacher.Name
                    })
                })
                .ToListAsync();
        }

        #endregion
    }
}