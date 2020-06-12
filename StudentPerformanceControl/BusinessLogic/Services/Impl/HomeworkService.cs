using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.EntityModels;
using DataCore.Exceptions;
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
        
        public async Task CreateHomeworkAsync(NewHomeworkDto homeworkDto)
        {
            var number = await _repository.GetAll<HomeworkInfo>()
                .Where(homework => homework.SubjectSettingId == homeworkDto.SubjectSettingsId)
                .CountAsync();

            number++;
            
            var dbHomework = new HomeworkInfo
            {
                SubjectSettingId = homeworkDto.SubjectSettingsId,
                MaxPoints = homeworkDto.MaxPoints,
                Number = number,
                Title = homeworkDto.HomeworkTitle
            };
            
            _repository.Add(dbHomework);

            await _repository.SaveContextAsync();

            var studentPerformanceIds = await _repository.GetAll<StudentPerformance>()
                .Where(performance => performance.Subject.SubjectSetting.SubjectSettingId == dbHomework.SubjectSettingId)
                .Select(performance => performance.StudentPerformanceId)
                .ToListAsync();

            foreach (var studentPerformanceId in studentPerformanceIds)
            {
                _repository.Add(new HomeworkResult
                {
                    HomeworkInfoId = dbHomework.HomeworkInfoId,
                    StudentPerformanceId = studentPerformanceId,
                    Points = 0
                });
            }

            await _repository.SaveContextAsync();
        }

        public async Task<HomeworkDto> GetHomeworkDtoAsync(int homeworkId)
        {
            return await _repository.GetAll<HomeworkInfo>()
                .Where(homework => homework.HomeworkInfoId == homeworkId)
                .Select(homework => new HomeworkDto
                {
                    HomeworkId = homeworkId,
                    HomeworkTitle = homework.Title,
                    MaxPoints = homework.MaxPoints,
                    SubjectId = homework.SubjectSetting.SubjectId
                })
                .SingleOrDefaultAsync()
                   ?? throw new SPCException($"Homework with id {homeworkId} does not exists", 404);
        }

        public async Task<IList<HomeworkDto>> GetHomeworksAsync(int subjectId)
        {
            return await _repository.GetAll<HomeworkInfo>()
                .Where(homework => homework.SubjectSetting.SubjectId == subjectId)
                .Select(homework => new HomeworkDto
                {
                    HomeworkId = homework.HomeworkInfoId,
                    HomeworkTitle = homework.Title,
                    Number = homework.Number,
                    MaxPoints = homework.MaxPoints,
                    SubjectId = homework.SubjectSetting.SubjectId
                })
                .ToListAsync();
        }

        public async Task EditHomeworkAsync(HomeworkDto homeworkDto)
        {
            var dbHomework = await _repository.GetAll<HomeworkInfo>()
                                 .SingleOrDefaultAsync(info => info.HomeworkInfoId == homeworkDto.HomeworkId)
                             ?? throw new SPCException($"Homework with id {homeworkDto.HomeworkId} is not exists", 404);

            dbHomework.Title = homeworkDto.HomeworkTitle;
            dbHomework.MaxPoints = homeworkDto.MaxPoints;
            
            _repository.Update(dbHomework);

            await _repository.SaveContextAsync();

        }

        public async Task DeleteHomeworkAsync(int homeworkId)
        {
            var dbHomework = await _repository.GetAll<HomeworkInfo>()
                                 .SingleOrDefaultAsync(info => info.HomeworkInfoId == homeworkId)
                             ?? throw new SPCException($"Homework with id {homeworkId} is not exists", 404);

            var dbHomeworkResults = await _repository.GetAll<HomeworkResult>()
                .Where(result => result.HomeworkInfoId == homeworkId)
                .ToListAsync();

            foreach (var dbHomeworkResult in dbHomeworkResults)
            {
                _repository.Delete(dbHomeworkResult);
            }
            
            _repository.Delete(dbHomework);

            await _repository.SaveContextAsync();
        }
    }
}