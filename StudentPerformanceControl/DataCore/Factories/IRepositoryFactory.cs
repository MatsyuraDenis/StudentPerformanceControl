using DataCore.Repository;

namespace DataCore.Factories
{
    public interface IRepositoryFactory
    {
        IRepository GerMsSqlRepository();
    }
}