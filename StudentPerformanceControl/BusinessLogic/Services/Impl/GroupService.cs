using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.EntityModels;
using DataCore.Factories;
using DataCore.Repository;
using Entity.Models.Dtos;
using Entity.Models.Dtos.Group;
using Entity.Models.Enums;
using Logger;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services.Impl
{
    public class GroupService : IGroupService
    {
        #region Dependencies

        private readonly IRepository _repository;
        private readonly ILogService _logService;

        #endregion

        #region ctor

        public GroupService(IRepositoryFactory repositoryFactory, ILogService logService)
        {
            _logService = logService;
            _repository = repositoryFactory.GetMsSqlRepository();
        }

        #endregion

        #region Implementation
        
        public async Task<IList<GroupDto>> GetGroupsAsync()
        {
            _logService.LogInfo("Start loading groups");
            
            var groups = await _repository.GetAll<Group>()
                .Where(group => group.GroupTypeId != (int) GroupTypes.Former)
                .Select(group => new GroupDto
                {
                    Id = group.GroupId,
                    Title = group.GroupName
                })
                .OrderBy(group => group.Title)
                .ToListAsync();
            
            _logService.LogInfo("Groups was loaded succesfull!");
            
            return groups;
        }

        public async Task<GroupDto> GetGroupAsync(int groupId)
        {
            return await GroupQuery()
                .SingleOrDefaultAsync(group => group.Id == groupId);
        }

        public async Task<int> AddGroupAsync(AddGroupDto group)
        {
            var newGroup = new Group
            {
                GroupName = group.GroupName,
                GroupTypeId = (int) GroupTypes.Created
            };
            
            _repository.Add(newGroup);
            await _repository.SaveContextAsync();
            
            return newGroup.GroupId;
        }

        public async Task DeactivateGroupAsync(int groupId)
        {
            var dbGroup = await _repository.GetAll<Group>()
                .SingleOrDefaultAsync(group => group.GroupId == groupId);

            dbGroup.GroupTypeId = (int) GroupTypes.Former;
            
            _repository.Update(dbGroup);

            await _repository.SaveContextAsync();
        }

        #endregion

        #region Private Methods

        private IQueryable<GroupDto> GroupQuery()
        {
            return _repository.GetAll<Group>()
                .Select(group => new GroupDto
                {
                    Id = group.GroupId,
                    Title = group.GroupName,
                    Type = group.GroupTypeId,
                    Subjects = group.Subjects.Select(subject => new SubjectDto
                    {
                        Id = subject.SubjectId,
                        SubjectName = subject.SubjectInfo.Title
                    }).OrderBy(subject => subject.SubjectName),
                    Students = group.Students.Select(student => new StudentDto
                    {
                        Id = student.StudentId,
                        Name = student.Name,
                        SecondName = student.SecondName
                    }).OrderBy(student => student.SecondName)
                });
        }

        #endregion
    }
}