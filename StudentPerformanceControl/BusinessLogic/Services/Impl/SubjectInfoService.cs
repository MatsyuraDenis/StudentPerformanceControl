using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.EntityModels;
using DataCore.Exceptions;
using DataCore.Factories;
using DataCore.Repository;
using Entity.Models.Dtos.Subject;
using Entity.Models.Enums;
using Logger;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services.Impl
{
    public class SubjectInfoService : ISubjectInfoService
    {
        #region Dependencies
        
        private readonly IRepository _repository;
        private readonly ILogService _logService;
        
        #endregion
        
        #region ctor

        public SubjectInfoService(IRepositoryFactory repositoryFactory, ILogService logService)
        {
            _logService = logService;
            _repository = repositoryFactory.GetMsSqlRepository();
        }
        
        #endregion

        #region Implementation

        public async Task DeleteSubjectInfoAsync(int subjectInfoId)
        {
            _logService.LogInfo($"Delete subject info with id {subjectInfoId}");
            
            var subjectInfo = await _repository.GetAll<SubjectInfo>()
                                          .SingleOrDefaultAsync(info => info.SubjectInfoId == subjectInfoId)
                                      ?? throw new SPCException($"Subject info with id {subjectInfoId} does bot exists", 404);
                    
            _repository.Delete(subjectInfo);
        
            await _repository.SaveContextAsync();
            
            _logService.LogInfo($"Subject info with id {subjectInfoId} deleted");
        }

        public async Task<IList<SubjectInfoDto>> GetSubjectInfosAsync()
        {
            _logService.LogInfo($"Load subject infos");
            
            var subjectInfos =  await _repository.GetAll<SubjectInfo>()
                .Select(info => new SubjectInfoDto
                {
                    Id = info.SubjectInfoId,
                    Title = info.Title,
                    GroupLearn = _repository.GetAll<Group>()
                        .Where(group => group.Subjects.Any(subject => subject.SubjectInfoId == info.SubjectInfoId)
                            && group.GroupTypeId == (int) GroupTypes.Active)
                        .Count(),
                    GroupLearned = _repository.GetAll<Group>()
                        .Where(group => group.Subjects.Any(subject => subject.SubjectInfoId == info.SubjectInfoId)
                                        && group.GroupTypeId == (int) GroupTypes.Former)
                        .Count()
                })
                .ToListAsync();

            _logService.LogInfo($"Subject infos loaded");
            
            return subjectInfos;
        }

         public async Task<SubjectInfoDto> GetSubjectInfoAsync(int subjectInfoId)
         {
             _logService.LogInfo($"Load subject info with id {subjectInfoId}");
             
             var dbSubjectInfo = await _repository.GetAll<SubjectInfo>()
                 .Where(subject => subject.SubjectInfoId == subjectInfoId)
                 .Select(subject => new SubjectInfoDto
                 {
                     Id = subjectInfoId,
                     Title = subject.Title
                 })
                 .SingleOrDefaultAsync()
                    ?? throw new SPCException($"Subject info with id {subjectInfoId} does bot exists", 404);
             
             _logService.LogInfo($"Subject info with id {subjectInfoId} loaded");

             return dbSubjectInfo;
         }

         public async Task CreateSubjectInfoAsync(SubjectInfoDto subjectInfoDto)
        {
            _logService.LogInfo($"Create subject info {subjectInfoDto.Title}");

            var newSubjectInfo = new SubjectInfo
            {
                Title = subjectInfoDto.Title
            };
            
            _repository.Add(newSubjectInfo);

            await _repository.SaveContextAsync();
            
            _logService.LogInfo($"Subject info {subjectInfoDto.Title} created with id {newSubjectInfo.SubjectInfoId}");
        }

        public async Task EditSubjectInfoAsync(SubjectInfoDto subjectInfoDto)
        {
            _logService.LogInfo($"Edit subject info with id {subjectInfoDto.Id}");
            
            var dbSubject = await _repository.GetAll<SubjectInfo>()
                                .Where(info => info.SubjectInfoId == subjectInfoDto.Id)
                                .SingleOrDefaultAsync()
                            ?? throw new SPCException($"Subject info with id {subjectInfoDto.Id} not exists", 404);

            dbSubject.Title = subjectInfoDto.Title;
            
            _repository.Update(dbSubject);

            await _repository.SaveContextAsync();
            
            _logService.LogInfo($"Subject info with id {subjectInfoDto.Id} edited");
        }

        public async Task<IList<SubjectInfoDto>> GetSubjectInfosAsync(int groupId)
        {
            _logService.LogInfo($"Load assigned subject infos for group {groupId}");
            
            var dbSubjectInfos =  await _repository.GetAll<SubjectInfo>()
                .Where(info => info.Subjects.All(subject => subject.GroupId != groupId))
                .Select(info => new SubjectInfoDto
                {
                    Id = info.SubjectInfoId,
                    Title = info.Title,
                    GroupLearn = _repository.GetAll<Group>()
                        .Where(group => group.Subjects.Any(subject => subject.SubjectInfoId == info.SubjectInfoId))
                        .Count()
                })
                .ToListAsync();
            
            _logService.LogInfo($"Assigned subject infos for group {groupId} loaded");

            return dbSubjectInfos;
        }
        
        #endregion
    }
}