using System.Threading.Tasks;
using Entity.Models.Dtos.PerformanceInfos;
using Entity.Models.Dtos.StudentPerformance;

namespace BusinessLogic.Services
{
    public interface IPerformanceService
    {
        Task EditPerformanceAsync(StudentPerformanceDto studentPerformanceDto);
        Task<StudentPerformanceDto> GetStudentPerformanceAsync(int studentId, int subjectId);
        Task<StudentPerformanceDetailsDto> GetStudentPerformanceAsync(int studentId);

    }
}