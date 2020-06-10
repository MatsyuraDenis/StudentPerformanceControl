using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.EntityModels;
using DataCore.Exceptions;
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
                .Select(group => new GroupDto
                {
                    Id = group.GroupId,
                    Title = group.GroupName,
                    Teacher = group.Curator.SecondName + group.Curator.Name
                    })
                .ToListAsync();
            
            _logService.LogInfo("Groups was loaded succesfull!");
            
            return groups;
        }

        public async Task<GroupDto> GetGroupAsync(int groupId)
        {
            return await GroupQuery()
                .SingleOrDefaultAsync(group => group.Id == groupId);
        }

        public async Task<int> AddGroup(AddGroupDto group)
        {
            var dbTeacher = await _repository.GetAll<Teacher>()
                .SingleOrDefaultAsync(teacher => teacher.TeacherId == group.TeacherId);

            if (dbTeacher.GroupId != null)
            {
                throw new SPCException($"Teacher {group.TeacherId} is curator for {dbTeacher.GroupId} group. Please select another teacher", 400);
            }

            var newGroup = new Group
            {
                CuratorId = group.TeacherId,
                GroupName = group.GroupName,
                GroupTypeId = (int) GroupTypes.Created
            };
            
            _repository.Add(newGroup);
            await _repository.SaveContextAsync();

            dbTeacher.GroupId = newGroup.GroupId;
            
            _repository.Update(dbTeacher);
            await _repository.SaveContextAsync();
            
            return newGroup.GroupId;
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
                    Subjects = group.Subjects.Select(subject => new SubjectDto
                    {
                        Id = subject.SubjectId,
                        SubjectName = subject.SubjectInfo.Title,
                        TeacherName = subject.Teacher.Name,
                        TeacherSecondName = subject.Teacher.SecondName,
                        TeacherId = subject.TeacherId,
                    }),
                    Students = group.Students.Select(student => new StudentDto
                    {
                        Id = student.StudentId,
                        Name = student.Name,
                        SecondName = student.SecondName
                    })
                });
        }

        #endregion
    }
}