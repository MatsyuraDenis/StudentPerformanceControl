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

        public async Task<StudentPerformanceDetailsDto> GetStudentPerformanceAsync(int studentId)
        {
            _logService.LogInfo($"Start loading academic performance for student with id {studentId}");

            var studentInfo = await _repository.GetAll<Student>()
                .Where(student => student.StudentId == studentId)
                .Select(student => new StudentPerformanceDetailsDto
                {
                    StudentId = student.StudentId,
                    Name = student.Name,
                    SecondName = student.SecondName,
                    Subjects = _repository.GetAll<StudentPerformance>()
                        .Where(performance => performance.StudentId == studentId && performance.Subject.GroupId == student.GroupId)
                        .Select(performance => new StudentSubjectPerformanceDto
                        {
                            SubjectId = performance.Subject.SubjectId,
                            SubjectTitle = performance.Subject.SubjectInfo.Title,
                            Module1Points = performance.Module1TestPoints,
                            Module2Points = performance.Module2TestPoints,
                            ExamPoints = performance.ExamPoints,
                            ExamMaxPoints = performance.Subject.ExamMaxPoints,
                            Module1MaxPoints = performance.Subject.Module1TestMaxPoints,
                            Module2MaxPoints = performance.Subject.Module2TestMaxPoints,
                            Homeworks = performance.HomeworkResults.Select(homework => new StudentHomeworkPerformanceDto
                            {
                                HomeworkId = homework.HomeworkInfoId,
                                HomeworkResultId = homework.HomeworkResultId,
                                Points = homework.Points,
                                MaxPoints = _repository.GetAll<HomeworkInfo>()
                                    .Where(info => info.HomeworkInfoId == homework.HomeworkInfoId)
                                    //Sum is only for 1 value
                                    .Sum(info => info.MaxPoints)
                            })
                        })
                })
                .SingleOrDefaultAsync()
                ?? throw new SPCException($"student with id {studentId} does not exists", StatusCodes.Status404NotFound);
            
            _logService.LogInfo($"Load academic performance for student with id {studentId} completed!");
            
            return studentInfo;
        }

        public async Task<StudentDto> GetStudentAsync(int studentId)
        {
            return await _repository.GetAll<Student>()
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
        }

        public async Task AddStudentAsync(StudentDto studentDto)
        {
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
        }

        public async Task EditStudentAsync(StudentDto studentDto)
        {
            var dbStudent = await _repository.GetAll<Student>()
                .SingleOrDefaultAsync(student => student.StudentId == studentDto.Id) 
                            ?? throw new SPCException($"student with id {studentDto.Id} does not exists", StatusCodes.Status404NotFound);

            dbStudent.Name = studentDto.Name;
            dbStudent.SecondName = studentDto.SecondName;
            
            _repository.Update(dbStudent);

            await _repository.SaveContextAsync();
        }

        public async Task RemoveStudentAsync(int studentId)
        {
            var dbStudent = await _repository.GetAll<Student>()
                .SingleOrDefaultAsync(student => student.StudentId == studentId) 
                            ?? throw new SPCException($"student with id {studentId} does not exists", StatusCodes.Status404NotFound);
            
            _repository.Delete(dbStudent);

            await _repository.SaveContextAsync();
        }

        #endregion
    }
}