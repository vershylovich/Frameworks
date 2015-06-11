namespace Adidas.Framework.Repository.EntityFramework.Factories
{
    using Adidas.Framework.Repository.Repositories;

    public interface IRepositoryProvider
    {
        IRepository<TEntity> GetRepositoryForEntityType<TEntity>() where TEntity : class;
    }
}
