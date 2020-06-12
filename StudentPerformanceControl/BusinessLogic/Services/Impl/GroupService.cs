using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.EntityModels;
using DataCore.Exceptions;
using DataCore.Factories;
using DataCore.Repository;
using Entity.Models.Dtos;
using Entity.Models.Dtos.Group;
using Entity.Models.Dtos.Subject;
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
        
        public async Task<IList<GroupDto>> GetGroupsAsync(int groupType)
        {
            _logService.LogInfo("Start loading groups");
            
            var groups = await _repository.GetAll<Group>()
                .Where(group => group.GroupTypeId == groupType)
                .Select(group => new GroupDto
                {
                    Id = group.GroupId,
                    CreatedAt = group.CreatedAt,
                    DeactivatedAt = group.DeactivatedAt,
                    Type = group.GroupTypeId,
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

        public async Task<int> BoostGroupAsync(int groupId)
        {
            var dbGroup = await _repository.GetAll<Group>()
                .Include(group => group.Students)
                .SingleOrDefaultAsync(group => group.GroupId == groupId);
            
            var newGroup = new Group
            {
                GroupTypeId = (int) GroupTypes.Created,
                GroupName = dbGroup.GroupName,
                CreatedAt = DateTime.UtcNow
            };
            
            _repository.Add(newGroup);

            await _repository.SaveContextAsync();

            foreach (var student in dbGroup.Students)
            {
                _repository.Add(new Student
                {
                    CommonId = student.CommonId == 0 ? student.StudentId : student.CommonId,
                    Name = student.Name,
                    SecondName = student.SecondName,
                    GroupId = newGroup.GroupId
                });
            }

            dbGroup.GroupTypeId = (int) GroupTypes.Former;
            dbGroup.DeactivatedAt = DateTime.UtcNow;
            
            _repository.Update(dbGroup);

            await _repository.SaveContextAsync();

            return newGroup.GroupId;
        }

        public async Task SaveAsync(int groupId)
        {
            var dbGroup = await _repository.GetAll<Group>()
                .SingleOrDefaultAsync(g => g.GroupId == groupId);

            dbGroup.CreatedAt = DateTime.UtcNow;
            dbGroup.GroupTypeId = (int) GroupTypes.Active;

            _repository.Update(dbGroup);
            
            await _repository.SaveContextAsync();
        }

        public async Task DeactivateGroupAsync(int groupId)
        {
            var dbGroup = await _repository.GetAll<Group>()
                .SingleOrDefaultAsync(group => group.GroupId == groupId);

            dbGroup.DeactivatedAt = DateTime.UtcNow;
            dbGroup.GroupTypeId = (int) GroupTypes.Former;
            
            _repository.Update(dbGroup);

            await _repository.SaveContextAsync();
        }

        public async Task DeleteGroupAsync(int groupId)
        {
            var dbGroup = await _repository.GetAll<Group>()
                              .Include(group => group.Students)
                              .Include(group => group.Subjects)
                              .SingleOrDefaultAsync(group => group.GroupId == groupId)
                          ?? throw new SPCException($"Group with id {groupId} does not exists", 404);

            foreach (var student in dbGroup.Students)
            {
                _repository.Delete(student);
            }
            
            foreach (var subject in dbGroup.Subjects)
            {
                _repository.Delete(subject);
            }

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
                    CreatedAt = group.CreatedAt,
                    DeactivatedAt = group.DeactivatedAt,
                    Subjects = group.Subjects.Select(subject => new SubjectDto
                    {
                        Id = subject.SubjectId,
                        SubjectName = subject.SubjectInfo.Title,
                        Module1MaxPoints = subject.SubjectSetting.Module1TestMaxPoints,
                        Module2MaxPoints = subject.SubjectSetting.Module2TestMaxPoints,
                        ExamMaxPoints = subject.SubjectSetting.ExamMaxPoints
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