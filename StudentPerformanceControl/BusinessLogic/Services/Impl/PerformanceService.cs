using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.EntityModels;
using DataCore.Exceptions;
using DataCore.Factories;
using DataCore.Repository;
using Entity.Models.Dtos.Homeworks;
using Entity.Models.Dtos.PerformanceInfos;
using Logger;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services.Impl
{
    public class PerformanceService : IPerformanceService
    {
        #region Dependencies

        private readonly IRepository _repository;
        private readonly ILogService _logService;

        #endregion

        #region ctor

        public PerformanceService(IRepositoryFactory repositoryFactory, ILogService logService)
        {
            _logService = logService;
            _repository = repositoryFactory.GetMsSqlRepository();
        }

        #endregion

        #region Implementation
        
        public async Task EditPerformanceAsync(StudentPerformanceDto studentPerformanceDto)
        {
            _logService.LogInfo($"Edit performance info for student {studentPerformanceDto.StudentId}, {studentPerformanceDto.StudentSecondName} {studentPerformanceDto.StudentName}");
            
            var studentPerformance = await _repository.GetAll<StudentPerformance>()
                                         .Where(performance =>
                                             performance.StudentId == studentPerformanceDto.StudentId &&
                                             performance.SubjectId == studentPerformanceDto.SubjectId)
                                         .Include(performance => performance.HomeworkResults)
                                         .SingleOrDefaultAsync()
                                     ?? throw new SPCException(
                                         $"Student performance for subject {studentPerformanceDto.SubjectId} and student {studentPerformanceDto.StudentId} not exists",
                                         404);

            studentPerformance.Module1TestPoints = studentPerformanceDto.Module1Result;
            studentPerformance.Module2TestPoints = studentPerformanceDto.Module2Result;
            studentPerformance.ExamPoints = studentPerformanceDto.ExamResult;

            _repository.Update(studentPerformance);
            
            foreach (var homework in studentPerformanceDto.EditableHomeworks)
            {
                var dbHomework = studentPerformance.HomeworkResults.SingleOrDefault(result =>
                    result.HomeworkResultId == homework.HomeworkResultId);

                if (dbHomework != null)
                {
                    dbHomework.Points = homework.Points;
                    _repository.Update(dbHomework);
                }
            }
            
            _logService.LogInfo($"Performance info for student {studentPerformanceDto.StudentId}, {studentPerformanceDto.StudentSecondName} {studentPerformanceDto.StudentName} edited");

            await _repository.SaveContextAsync();
        }

        public async Task<StudentPerformanceDto> GetStudentPerformanceAsync(int studentId, int subjectId)
        {
            _logService.LogInfo($"Load performance info for student {studentId}, subject {subjectId}");

            var studentPerformances = await _repository.GetAll<Subject>()
                .Where(subject => subject.SubjectId == subjectId)
                .Select(subject => subject.StudentPerformances
                    .Where(performance => performance.StudentId == studentId)
                    .Select(performance => new StudentPerformanceDto
                    {
                        StudentId = performance.StudentId,
                        SubjectId = performance.SubjectId,
                        StudentName = performance.Student.Name,
                        StudentSecondName = performance.Student.SecondName,
                        ExamResult = performance.ExamPoints,
                        Module1Result = performance.Module1TestPoints,
                        Module2Result = performance.Module2TestPoints,
                        Module1Max = performance.Subject.Module1TestMaxPoints,
                        Module2Max = performance.Subject.Module2TestMaxPoints,
                        ExamMax = performance.Subject.ExamMaxPoints,
                        Homeworks = performance.HomeworkResults.Select(result => new HomeworkResultDto
                        {
                            HomeworkId = result.HomeworkInfoId,
                            HomeworkResultId = result.HomeworkResultId,
                            Points = result.Points
                        })
                    }))
                    .SingleOrDefaultAsync();

            var studentPerformance = studentPerformances.SingleOrDefault() ??
                                     throw new SPCException($"Student {studentId} don't have results for subject {subjectId}", 404);

            await AssignValueToHomeworksAsync(studentPerformance);
            
            _logService.LogInfo($"Performance info for student {studentId}, subject {subjectId} loaded");
            
            return studentPerformance;
        }
        
        #endregion

        #region Private methods

        private async Task AssignValueToHomeworksAsync(StudentPerformanceDto studentPerformance)
        {
            var homeworkIds = studentPerformance.Homeworks.Select(dto => dto.HomeworkId).ToList();

            var homeworksData = await _repository.GetAll<HomeworkInfo>()
                .Where(homework => homeworkIds.Contains(homework.HomeworkInfoId))
                .Select(homework => new
                {
                    HomeworkId = homework.HomeworkInfoId,
                    Number = homework.Number,
                    MaxPoints = homework.MaxPoints
                })
                .ToListAsync();

            studentPerformance.EditableHomeworks = studentPerformance.Homeworks.ToList();

            foreach (var editableHomework in studentPerformance.EditableHomeworks)
            {
                var dbHomework =
                    homeworksData.SingleOrDefault(homework => editableHomework.HomeworkId == homework.HomeworkId);

                if (dbHomework != null)
                {
                    editableHomework.MaxPoints = dbHomework.MaxPoints;
                    editableHomework.HomeworkNumber = dbHomework.Number;
                }
            }
        }

        #endregion
    }
}