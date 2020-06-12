using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.Models.Dtos.PerformanceInfos;
using Entity.Models.Dtos.Subject;

namespace BusinessLogic.Services
{
    public interface ISubjectService
    {
        Task<SubjectPerformanceInfoDto>  GetSubjectPerformanceInfoAsync(int subjectId);
        Task<SubjectTestDto> GetSubjectAsync(int subjectId);
        Task CreateSubjectAsync(SubjectDto subjectDto);
        Task EditSubjectAsync(SubjectDto subjectDto);
        Task RemoveSubjectAsync(int subjectId);
    }
}