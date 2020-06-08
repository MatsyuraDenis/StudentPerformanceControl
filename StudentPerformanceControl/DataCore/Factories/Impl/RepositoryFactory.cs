using DataCore.Contexts;
using DataCore.Repository;

namespace DataCore.Factories.Impl
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public IRepository GetMsSqlRepository()
        {
            return new Repository.Impl.Repository(new SPCContext());
        }
    }
}