using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.EntityModels;
using DataCore.Factories;
using DataCore.Repository;
using Entity.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services.Impl
{
    public class GroupService : IGroupService
    {
        #region Dependencies

        private readonly IRepository _repository;

        #endregion

        #region ctor

        public GroupService(IRepositoryFactory repositoryFactory)
        {
            _repository = repositoryFactory.GetMsSqlRepository();
        }

        #endregion

        #region Implementation
        
        public async Task<IList<GroupDto>> GetGroupsAsync()
        {
            return await GroupQuery()
                .ToListAsync();
        }

        public async Task<GroupDto> GetGroupAsync(int groupId)
        {
            return await GroupQuery()
                .SingleOrDefaultAsync(group => group.Id == groupId);
        }
        
        #endregion

        #region Private Methods

        private IQueryable<GroupDto> GroupQuery()
        {
            return _repository.GetAll<Group>()
                .Select(group => new GroupDto
                {
                    Id = group.GroupId,
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