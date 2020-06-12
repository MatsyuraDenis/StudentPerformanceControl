using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.EntityModels;
using DataCore.Exceptions;
using DataCore.Factories;
using DataCore.Repository;
using Entity.Models.Dtos.Subject;
using Entity.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services.Impl
{
    public class SubjectInfoService : ISubjectInfoService
    {
        #region Dependencies
        
        private readonly IRepository _repository;
        
        #endregion
        
        #region ctor

        public SubjectInfoService(IRepositoryFactory repositoryFactory)
        {
            _repository = repositoryFactory.GetMsSqlRepository();
        }
        
        #endregion

        #region Implementation

        public async Task DeleteSubjectInfoAsync(int subjectInfoId)
        {
            var subjectInfo = await _repository.GetAll<SubjectInfo>()
                                          .SingleOrDefaultAsync(info => info.SubjectInfoId == subjectInfoId)
                                      ?? throw new SPCException($"Subject info with id {subjectInfoId} does bot exists", 404);
                    
            _repository.Delete(subjectInfo);
        
            await _repository.SaveContextAsync();
        }

        public async Task<IList<SubjectInfoDto>> GetSubjectInfosAsync()
        {
            return await _repository.GetAll<SubjectInfo>()
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
        }

         public async Task<SubjectInfoDto> GetSubjectInfoAsync(int subjectInfoId)
         {
             return await _repository.GetAll<SubjectInfo>()
                 .Where(subject => subject.SubjectInfoId == subjectInfoId)
                 .Select(subject => new SubjectInfoDto
                 {
                     Id = subjectInfoId,
                     Title = subject.Title
                 })
                 .SingleOrDefaultAsync()
                    ?? throw new SPCException($"Subject info with id {subjectInfoId} does bot exists", 404);
         }

         public async Task CreateSubjectInfoAsync(SubjectInfoDto subjectInfoDto)
        {
            _repository.Add(new SubjectInfo
            {
                Title = subjectInfoDto.Title
            });

            await _repository.SaveContextAsync();
        }

        public async Task EditSubjectInfoAsync(SubjectInfoDto subjectInfoDto)
        {
            var dbSubject = await _repository.GetAll<SubjectInfo>()
                                .Where(info => info.SubjectInfoId == subjectInfoDto.Id)
                                .SingleOrDefaultAsync()
                            ?? throw new SPCException($"Subject info with id {subjectInfoDto.Id} not exists", 404);

            dbSubject.Title = subjectInfoDto.Title;
            
            _repository.Update(dbSubject);

            await _repository.SaveContextAsync();
        }

        public async Task<IList<SubjectInfoDto>> GetSubjectInfosAsync(int groupId)
        {
            return await _repository.GetAll<SubjectInfo>()
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
        }
        
        #endregion
    }
}