using System.Linq;
using System.Threading.Tasks;
using DataCore.EntityModels;
using DataCore.Factories;
using DataCore.Repository;
using Entity.Models.Dtos;

namespace BusinessLogic.Services
{
    public class PerformanceService : IPerformanceService
    {
        #region Dependencies

        private readonly IRepository _repository;
        
        #endregion
        
        #region ctor

        public PerformanceService(IRepositoryFactory repositoryFactory)
        {
            _repository = repositoryFactory.GetMsSqlRepository();
        }
        
        #endregion
        
        #region Implementation

        // public Task<SubjectPerformanceInfoDto> GetSubjectPerformanceInfoAsync(int subjectId)
        // {
        //     var dbResult = await _repository.GetAll<Subject>()
        //         .Select(subject => new
        //         {
        //             Id = subjectId,
        //             TeacherName = subject.Teacher.Name,
        //             TeacherSecondName = subject.Teacher.SecondName,
        //             Laboratories = subject.Modules.Select(module => module.Laboratories.Select(laboratory => new StudentHomeworkDto
        //             {
        //                 Id = laboratory.LaboratoryId,
        //                 Points = laboratory.StudentGrades.FirstOrDefault(grade => grade.StudentId == )
        //             }))
        //         })
        // }

        #endregion
    }
}