namespace Adidas.Framework.Repository.Repositories
{
    using System.Collections.Generic;

    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Find(params object[] ids);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(params object[] ids);
        void Delete(TEntity entity);

        IRepositoryQuery<TEntity> Query();
    }
}
