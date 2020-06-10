using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.EntityModels;
using DataCore.Exceptions;
using DataCore.Factories;
using DataCore.Repository;
using Entity.Models.Dtos.Teacher;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services.Impl
{
    public class TeacherService : ITeacherService
    {
        private readonly IRepository _repository;

        public TeacherService(IRepositoryFactory repositoryFactory)
        {
            _repository = repositoryFactory.GetMsSqlRepository();
        }

        public async Task<IList<TeacherDto>> GetPossibleCuratorAsync()
        {
            return await _repository.GetAll<Teacher>()
                .Where(teacher => teacher.GroupId == null)
                .Select(teacher => new TeacherDto
                {
                    Id = teacher.TeacherId,
                    Fullname = teacher.SecondName + " " + teacher.Name
                })
                .OrderBy(curator => curator.Fullname)
                .ToListAsync();
        }

        public async Task AddSubjectForTeacherAsync(int teacherId, int subjectId)
        {
            if (await _repository.GetAll<TeacherSubjectInfo>()
                .AnyAsync(info => info.TeacherId == teacherId && info.SubjectInfoId == subjectId))
            {
                throw new SPCException($"Teacher with id {teacherId} already asigned for subjects {subjectId}", 400);
            }
            
            _repository.Add( new TeacherSubjectInfo
            {
                TeacherId = teacherId,
                SubjectInfoId = subjectId
            });

            await _repository.SaveContextAsync();
        }
    }
}