using System.Linq;
using System.Threading.Tasks;
using DataCore.EntityModels;
using DataCore.Factories;
using DataCore.Repository;
using Entity.Models.Dtos.Homeworks;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services.Impl
{
    public class HomeworkService : IHomeworkService
    {
        private readonly IRepository _repository;

        public HomeworkService(IRepositoryFactory repositoryFactory)
        {
            _repository = repositoryFactory.GetMsSqlRepository();
        }
        
        public async Task CreateHomework(NewHomeworkDto homeworkDto)
        {
            var homework = new HomeworkInfo
            {
                SubjectSettingId = homeworkDto.SubjectSettingsId,
                MaxPoints = homeworkDto.MaxPoints,
                Number = homeworkDto.HomeworkNumber,
                Title = homeworkDto.HomeworkTitle
            };
            
            _repository.Add(homework);

            await _repository.SaveContextAsync();

            var studentPerformanceIds = await _repository.GetAll<StudentPerformance>()
                .Where(performance => performance.Subject.SubjectSetting.SubjectSettingId == homework.SubjectSettingId)
                .Select(performance => performance.StudentPerformanceId)
                .ToListAsync();

            foreach (var studentPerformanceId in studentPerformanceIds)
            {
                _repository.Add(new HomeworkResult
                {
                    HomeworkInfoId = homework.HomeworkInfoId,
                    StudentPerformanceId = studentPerformanceId,
                    Points = 0
                });
            }

            await _repository.SaveContextAsync();
        }
    }
}