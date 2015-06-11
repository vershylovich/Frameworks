namespace Adidas.Framework.Service.Services
{
    using System.Collections.Generic;

    using Adidas.Framework.Repository.Repositories;

    public interface IService<TEntity> where TEntity : class
    {
        TEntity FindById(params object[] ids);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(params object[] ids);
        void Delete(TEntity entity);

        IRepositoryQuery<TEntity> Query();
    }
}
