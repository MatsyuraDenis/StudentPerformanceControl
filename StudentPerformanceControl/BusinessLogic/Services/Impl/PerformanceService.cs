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
    }
}