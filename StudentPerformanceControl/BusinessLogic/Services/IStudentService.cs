using System.Collections;
using System.Threading.Tasks;
using Entity.Models.Dtos.StudentPerformance;

namespace BusinessLogic.Services
{
    public interface IStudentService
    {
        Task<StudentPerformanceDetailsDto> GetStudentPerformanceAsync(int studentId);
    }
}