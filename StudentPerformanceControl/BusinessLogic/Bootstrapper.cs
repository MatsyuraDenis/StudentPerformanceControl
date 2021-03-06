﻿using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.Services;
using BusinessLogic.Services.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic
{
    public static class Bootstrapper
    {
        public static void AddBusinessLogic(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<ISubjectService, SubjectService>();
            services.AddTransient<IPerformanceService, PerformanceService>();
            services.AddTransient<IHomeworkService, HomeworkService>();
            services.AddTransient<ISubjectInfoService, SubjectInfoService>();
        }
    }
}