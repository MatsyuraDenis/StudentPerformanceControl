using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataCore.EntityModels;
using Entity.Models.Dtos;
using Entity.Models.Dtos.StudentPerformance;

namespace BusinessLogic.Services
{
    public interface IStudentService
    {
        Task<StudentDto> GetStudentAsync(int studentId);
        Task AddStudentAsync(StudentDto studentDto);
        Task EditStudentAsync(StudentDto studentDto);
        Task RemoveStudentAsync(int studentId);
    }
}