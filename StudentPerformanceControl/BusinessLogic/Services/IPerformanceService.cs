using System.Threading.Tasks;
using Entity.Models.Dtos.PerformanceInfos;

namespace BusinessLogic.Services
{
    public interface IPerformanceService
    {
        Task EditPerformanceAsync(StudentPerformanceDto studentPerformanceDto);
    }
}