using System.Threading.Tasks;
using Entity.Models.Dtos.Homeworks;

namespace BusinessLogic.Services
{
    public interface IHomeworkService
    {
        Task CreateHomeworkAsync(NewHomeworkDto homeworkDto);
        Task<HomeworkDto> GetHomeworkDtoAsync(int homeworkId);
        Task<HomeworkIndexDto> GetHomeworksAsync(int subjectId);
        Task EditHomeworkAsync(HomeworkDto homeworkDto);
        Task DeleteHomeworkAsync(int homeworkId);
        Task<NewHomeworkDataDto> GetCreateHomeworkDataAsync(int subjectId);
    }
}