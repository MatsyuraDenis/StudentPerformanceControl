using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.EntityModels;
using DataCore.Factories;
using DataCore.Repository;
using Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace StudentPerformanceControl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogService _logService;
        private readonly IRepository _repository;

        public WeatherForecastController(ILogService logService, IRepositoryFactory repositoryFactory)
        {
            _logService = logService;
            _repository = repositoryFactory.GerMsSqlRepository();
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logService.LogInfo("i am log");
            var users = _repository.GetAll<Teacher>().ToList();
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
