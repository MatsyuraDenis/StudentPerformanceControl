using System.Collections.Generic;
using System.Threading.Tasks;
using DataCore.EntityModels;
using Entity.Models.Dtos.Teacher;

namespace BusinessLogic.Services
{
    public interface ITeacherService
    {
        Task<IList<TeacherDto>> GetPossibleCuratorAsync();
        Task AddSubjectForTeacherAsync(int teacherId, int subjectId);
    }
}