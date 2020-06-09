using System.Threading.Tasks;
using Entity.Models.Dtos.PerformanceInfos;

namespace BusinessLogic.Services
{
    public interface ISubjectService
    {
        Task<SubjectPerformanceInfoDto>  GetSubjectPerformanceInfoAsync(int subjectId);
    }
}