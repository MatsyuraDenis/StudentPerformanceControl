using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.Models.Dtos.Subject;

namespace BusinessLogic.Services
{
    public interface ISubjectInfoService
    {
        Task<IList<SubjectInfoDto>> GetSubjectInfosAsync();
        Task CreateSubjectInfoAsync(SubjectInfoDto subjectInfoDto);
        Task EditSubjectInfoAsync(SubjectInfoDto subjectInfoDto);
        Task<IList<SubjectInfoDto>> GetSubjectInfosAsync(int groupId);
        Task DeleteSubjectInfoAsync(int subjectInfoId);
    }
}