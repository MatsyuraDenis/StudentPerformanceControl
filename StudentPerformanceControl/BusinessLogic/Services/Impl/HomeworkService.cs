using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.EntityModels;
using DataCore.Exceptions;
using DataCore.Factories;
using DataCore.Repository;
using Entity.Models.Dtos.Homeworks;
using Logger;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services.Impl
{
    public class HomeworkService : IHomeworkService
    {
        private readonly IRepository _repository;
        private readonly ILogService _logService;

        public HomeworkService(IRepositoryFactory repositoryFactory, ILogService logService)
        {
            _logService = logService;
            _repository = repositoryFactory.GetMsSqlRepository();
        }
        
        public async Task CreateHomeworkAsync(NewHomeworkDto homeworkDto)
        {
            _logService.LogInfo($"Start creating homework {homeworkDto.HomeworkTitle} for group with id {homeworkDto.GroupId}, subject {homeworkDto.SubjectId}");
            
            var number = await _repository.GetAll<HomeworkInfo>()
                .Where(homework => homework.SubjectId == homeworkDto.SubjectId)
                .CountAsync();

            number++;
            
            var dbHomework = new HomeworkInfo
            {
                SubjectId = homeworkDto.SubjectId,
                MaxPoints = homeworkDto.MaxPoints,
                Number = number,
                Title = homeworkDto.HomeworkTitle
            };
            
            _repository.Add(dbHomework);

            await _repository.SaveContextAsync();

            var studentPerformanceIds = await _repository.GetAll<StudentPerformance>()
                .Where(performance => performance.Subject.SubjectId == dbHomework.SubjectId)
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

            _logService.LogInfo($"Homework {homeworkDto.HomeworkTitle} for group with id {homeworkDto.GroupId}, subject {homeworkDto.SubjectId} created");
            
            await _repository.SaveContextAsync();
        }

        public async Task<HomeworkDto> GetHomeworkDtoAsync(int homeworkId)
        {
            _logService.LogInfo($"Loading homework {homeworkId}");
            
            var dbHomework =  await _repository.GetAll<HomeworkInfo>()
                .Where(homework => homework.HomeworkInfoId == homeworkId)
                .Select(homework => new HomeworkDto
                {
                    HomeworkId = homeworkId,
                    SubjectName = homework.Subject.SubjectInfo.Title,
                    GroupId = homework.Subject.GroupId,
                    HomeworkTitle = homework.Title,
                    MaxPoints = homework.MaxPoints,
                    SubjectId = homework.SubjectId
                })
                .SingleOrDefaultAsync()
                   ?? throw new SPCException($"Homework with id {homeworkId} does not exists", 404);
            
            _logService.LogInfo($"Homework {homeworkId} load completed");
            
            return dbHomework;
        }

        public async Task<HomeworkIndexDto> GetHomeworksAsync(int subjectId)
        {
            _logService.LogInfo($"Loading homeworks for subject {subjectId}");
            
            var homeworks =  await _repository.GetAll<HomeworkInfo>()
                .Where(homework => homework.SubjectId == subjectId)
                .Select(homework => new HomeworkDto
                {
                    HomeworkId = homework.HomeworkInfoId,
                    HomeworkTitle = homework.Title,
                    Number = homework.Number,
                    MaxPoints = homework.MaxPoints,
                    SubjectId = homework.SubjectId
                })
                .ToListAsync();

            var data = await _repository.GetAll<Subject>()
                .Where(subject => subject.SubjectId == subjectId)
                .Select(subject => new
                {
                    Subject = subject.SubjectInfo.Title,
                    Group = subject.Group.GroupName,
                    GroupId = subject.GroupId,
                    TestExamSum = subject.Module1TestMaxPoints
                        + subject.Module2TestMaxPoints
                        + subject.ExamMaxPoints
                })
                .SingleOrDefaultAsync();
            
            var index = new HomeworkIndexDto
            {
                Homeworks = homeworks,
                SubjectName = data.Subject,
                GroupName = data.Group,
                GroupId = data.GroupId,
                TestExamSum = data.TestExamSum,
                HomeworkSum = homeworks.Sum(dto => dto.MaxPoints),
                TotalPoints = homeworks.Sum(dto => dto.MaxPoints) + data.TestExamSum
            };

            _logService.LogInfo($"Homeworks for subject {subjectId} loaded");
            
            return index;
        }

        public async Task EditHomeworkAsync(HomeworkDto homeworkDto)
        {
            _logService.LogInfo($"Edit homework {homeworkDto.HomeworkId}");
            
            var dbHomework = await _repository.GetAll<HomeworkInfo>()
                                 .SingleOrDefaultAsync(info => info.HomeworkInfoId == homeworkDto.HomeworkId)
                             ?? throw new SPCException($"Homework with id {homeworkDto.HomeworkId} is not exists", 404);

            dbHomework.Title = homeworkDto.HomeworkTitle;
            dbHomework.MaxPoints = homeworkDto.MaxPoints;
            
            _repository.Update(dbHomework);
            
            _logService.LogInfo($"Homework {homeworkDto.HomeworkId} edited");

            await _repository.SaveContextAsync();

        }

        public async Task DeleteHomeworkAsync(int homeworkId)
        {
            _logService.LogInfo($"Delete homework {homeworkId}");
            
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

            _logService.LogInfo($"Homework {homeworkId} deleted");
            
            await _repository.SaveContextAsync();
        }

        public async Task<NewHomeworkDataDto> GetCreateHomeworkDataAsync(int subjectId)
        {
            _logService.LogInfo($"Get homework data for subject  {subjectId}");
            
            var data =  await _repository.GetAll<Subject>()
                       .Where(subject => subject.SubjectId == subjectId)
                       .Select(subject => new NewHomeworkDataDto
                       {
                           SubjectName = subject.SubjectInfo.Title,
                           SubjectId = subject.SubjectId,
                           GroupName = subject.Group.GroupName
                       })
                       .SingleOrDefaultAsync()
                   ?? throw new SPCException($"Subject with id {subjectId} does not exists in database", 404);
            
            _logService.LogInfo($"Homework data for subject {subjectId} loaded");

            return data;
        }
    }
}