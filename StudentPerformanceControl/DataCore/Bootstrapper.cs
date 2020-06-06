using DataCore.Factories;
using DataCore.Factories.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace DataCore
{
    public static class Bootstrapper
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddTransient<IRepositoryFactory, RepositoryFactory>();
        }
    }
}