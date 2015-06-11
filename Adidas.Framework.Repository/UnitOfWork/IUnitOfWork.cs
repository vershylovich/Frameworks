namespace Adidas.Framework.Repository.UnitOfWork
{
    using System;

    using Adidas.Framework.Repository.Repositories;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        void Commit();
        Guid InstanceId { get; }
    }
}
