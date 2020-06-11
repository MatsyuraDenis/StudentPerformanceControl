using System.Collections;
using System.Threading.Tasks;
using DataCore.EntityModels;
using Entity.Models.Dtos;
using Entity.Models.Dtos.StudentPerformance;

namespace BusinessLogic.Services
{
    public interface IStudentService
    {
        Task<StudentPerformanceDetailsDto> GetStudentPerformanceAsync(int studentId);
        Task AddStudentAsync(StudentDto studentDto);
        Task RemoveStudentAsync(int studentId);
    }
}