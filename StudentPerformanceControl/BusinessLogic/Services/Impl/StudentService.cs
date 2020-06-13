using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.EntityModels;
using DataCore.Exceptions;
using DataCore.Factories;
using DataCore.Repository;
using Entity.Models.Dtos;
using Entity.Models.Dtos.PerformanceInfos;
using Entity.Models.Dtos.StudentPerformance;
using Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services.Impl
{
    public class StudentService : IStudentService
    {
        #region Dependencies

        private readonly IRepository _repository;
        private readonly ILogService _logService;

        #endregion

        #region ctor

        public StudentService(IRepositoryFactory repositoryFactory, ILogService logService)
        {
            _logService = logService;
            _repository = repositoryFactory.GetMsSqlRepository();
        }

        #endregion

        #region Methods

        public async Task<StudentDto> GetStudentAsync(int studentId)
        {
            _logService.LogInfo($"Load student with id {studentId}");
            
            var dbStudent =  await _repository.GetAll<Student>()
                .Where(student => student.StudentId == studentId)
                .Select(student => new StudentDto
                {
                    GroupId = student.GroupId,
                    Name = student.Name,
                    SecondName = student.SecondName,
                    Id = student.StudentId
                })
                .SingleOrDefaultAsync()
                ?? throw new SPCException($"student with id {studentId} does not exists", StatusCodes.Status404NotFound);
            
            _logService.LogInfo($"Student with id {studentId} loaded");

            return dbStudent;
        }

        public async Task AddStudentAsync(StudentDto studentDto)
        {
            _logService.LogInfo($"Add student {studentDto.SecondName} {studentDto.Name} for group {studentDto.GroupId}");
            
            var student = new Student
            {
                Name = studentDto.Name,
                SecondName = studentDto.SecondName,
                GroupId = studentDto.GroupId
            };
            
            _repository.Add(student);

            await _repository.SaveContextAsync();

            var subjectHomeworksIds = await _repository.GetAll<Subject>()
                .Where(subject => subject.GroupId == student.GroupId)
                .Select(subject => new
                {
                    SubjectId = subject.SubjectId,
                    HomeworkIds = subject.HomeworkInfos.Select(homework => homework.HomeworkInfoId)
                })
                .ToListAsync();

            var studentPerformances = subjectHomeworksIds.Select(subject => new StudentPerformance
                {
                    SubjectId = subject.SubjectId,
                    ExamPoints = 0,
                    Module1TestPoints = 0,
                    Module2TestPoints = 0,
                    StudentId = student.StudentId,
                    HomeworkResults = subject.HomeworkIds.Select(homework => new HomeworkResult
                    {
                        Points = 0,
                        HomeworkInfoId = homework
                    }).ToList()
                })
                .ToList();

            foreach (var studentPerformance in studentPerformances)
            {
                _repository.Add(studentPerformance);
            }
            
            await _repository.SaveContextAsync();
            
            _logService.LogInfo($"Student {studentDto.SecondName} {studentDto.Name} for group {studentDto.GroupId} added");
        }

        public async Task EditStudentAsync(StudentDto studentDto)
        {
            _logService.LogInfo($"Edit Student {studentDto.Id}, {studentDto.SecondName} {studentDto.Name} for group {studentDto.GroupId}");
            
            var dbStudent = await _repository.GetAll<Student>()
                .SingleOrDefaultAsync(student => student.StudentId == studentDto.Id) 
                            ?? throw new SPCException($"student with id {studentDto.Id} does not exists", StatusCodes.Status404NotFound);

            dbStudent.Name = studentDto.Name;
            dbStudent.SecondName = studentDto.SecondName;
            
            _repository.Update(dbStudent);

            await _repository.SaveContextAsync();
            
            _logService.LogInfo($"Student {studentDto.Id}, {studentDto.SecondName} {studentDto.Name} for group {studentDto.GroupId} edited");
        }

        public async Task RemoveStudentAsync(int studentId)
        {
            _logService.LogInfo($"Remove student {studentId}");
            
            var dbStudent = await _repository.GetAll<Student>()
                .SingleOrDefaultAsync(student => student.StudentId == studentId) 
                            ?? throw new SPCException($"student with id {studentId} does not exists", StatusCodes.Status404NotFound);
            
            _repository.Delete(dbStudent);

            await _repository.SaveContextAsync();
            
            _logService.LogInfo($"Student {studentId} removed");
        }

        #endregion
    }
}