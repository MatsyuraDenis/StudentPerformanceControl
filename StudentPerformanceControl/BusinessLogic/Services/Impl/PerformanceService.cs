using System.Linq;
using System.Threading.Tasks;
using DataCore.EntityModels;
using DataCore.Exceptions;
using DataCore.Factories;
using DataCore.Repository;
using Entity.Models.Dtos.PerformanceInfos;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services.Impl
{
    public class PerformanceService : IPerformanceService
    {
        private readonly IRepository _repository;

        public PerformanceService(IRepositoryFactory repositoryFactory)
        {
            _repository = repositoryFactory.GetMsSqlRepository();
        }

        public async Task EditPerformanceAsync(StudentPerformanceDto studentPerformanceDto)
        {
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

            await _repository.SaveContextAsync();
        }

        public async Task<StudentPerformanceDto> GetStudentPerformanceAsync(int studentId, int subjectId)
        {
            var studentPerformance = await _repository.GetAll<Subject>()
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
                        Homeworks = performance.HomeworkResults.Select(result => new HomeworkResultDto
                        {
                            HomeworkId = result.HomeworkInfoId,
                            HomeworkResultId = result.HomeworkResultId,
                            HomeworkNumber = _repository.GetAll<HomeworkInfo>()
                                .Where(info => info.HomeworkInfoId == result.HomeworkInfoId)
                                .Sum(info => info.Number),
                            Points = result.Points
                        })
                    }))
                    .SingleOrDefaultAsync();

            var res = studentPerformance.SingleOrDefault() ??
                      throw new SPCException($"Student {studentId} don't have results for subject {subjectId}", 404);

            res.EditableHomeworks = res.Homeworks.ToList();
            
            return res;
        }
    }
}