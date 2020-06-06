using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
using NLog.Web;

namespace Logger
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddLoggerService(this IServiceCollection services, IConfiguration config) 
        {
            services.AddLogging(loggingBuilder => ConfigureExtensions.AddNLog(loggingBuilder, "nlog.config"));
            services.AddSingleton<ILogService, NLogService>();

            return services;
        }
    }
}