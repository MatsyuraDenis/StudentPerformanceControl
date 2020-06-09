using System.Linq;
using System.Threading.Tasks;
using DataCore.EntityModels;
using DataCore.Exceptions;
using DataCore.Factories;
using DataCore.Repository;
using Entity.Models.Dtos.PerformanceInfos;
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
        
        #endregion
    }
}