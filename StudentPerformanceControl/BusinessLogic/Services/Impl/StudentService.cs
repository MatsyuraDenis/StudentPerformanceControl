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
                            ExamMaxPoints = performance.Subject.SubjectSetting.ExamMaxPoints,
                            Module1MaxPoints = performance.Subject.SubjectSetting.Module1TestMaxPoints,
                            Module2MaxPoints = performance.Subject.SubjectSetting.Module2TestMaxPoints,
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

        public async Task AddStudentAsync(StudentDto studentDto)
        {
            _repository.Add(new Student
            {
                Name = studentDto.Name,
                SecondName = studentDto.SecondName,
                GroupId = studentDto.GroupId
            });

            await _repository.SaveContextAsync();
        }

        #endregion
    }
}