using System.Threading.Tasks;
using Entity.Models.Dtos.Homeworks;

namespace BusinessLogic.Services
{
    public interface IHomeworkService
    {
        Task CreateHomework(NewHomeworkDto homeworkDto);
    }
}