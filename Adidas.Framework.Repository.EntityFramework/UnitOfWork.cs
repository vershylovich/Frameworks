namespace Adidas.Framework.Repository.EntityFramework
{
    using System;

    using Adidas.Framework.Repository.EntityFramework.Factories;
    using Adidas.Framework.Repository.Repositories;
    using Adidas.Framework.Repository.UnitOfWork;

    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextBase context;
        private readonly IRepositoryProvider repositoryProvider;
        private bool disposed;

        public Guid InstanceId { get; private set; }

        public UnitOfWork(DbContextBase context, IRepositoryProvider repositoryProvider)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException("repositoryProvider");
            }

            this.context = context;
            this.context.Configuration.LazyLoadingEnabled = false;
            this.repositoryProvider = repositoryProvider;
            this.InstanceId = Guid.NewGuid();
        }

        public UnitOfWork(DbContextBase context)
            : this(context, new DefaultRepositoryProvider(context))
        {
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return this.repositoryProvider.GetRepositoryForEntityType<TEntity>();
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
            if (this.disposed)
                return;

            this.context.Dispose();
            this.disposed = true;
        }
    }
}
