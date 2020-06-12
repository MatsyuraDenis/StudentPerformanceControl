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
        Task EditSubjectAsync(NewSubjectDto subjectDto);
        Task DeleteSubjectInfoAsync(int subjectInfoId);
        Task RemoveSubjectAsync(int subjectId);
        Task<IList<SubjectInfoDto>> GetSubjectInfosAsync();
        Task CreateSubjectInfoAsync(SubjectInfoDto subjectInfoDto);
        Task EditSubjectInfoAsync(SubjectInfoDto subjectInfoDto);
        Task<IList<SubjectInfoDto>> GetSubjectInfosAsync(int groupId);
    }
}