using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.Models.Dtos.PerformanceInfos;
using Entity.Models.Dtos.Subject;

namespace BusinessLogic.Services
{
    public interface ISubjectService
    {
        Task<SubjectPerformanceInfoDto>  GetSubjectPerformanceInfoAsync(int subjectId);
        Task CreateSubjectAsync(NewSubjectDto subjectDto);
        Task RemoveSubjectAsync(int subjectId);
        Task<IList<SubjectInfoDto>> GetSubjectInfosAsync();
        Task<IList<SubjectInfoDto>> GetSubjectInfosAsync(int groupId);
    }
}