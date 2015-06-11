namespace Adidas.Framework.Repository.EntityFramework.Factories
{
    using System;

    using Adidas.Framework.Repository.Repositories;

    public sealed class DefaultRepositoryProvider : IRepositoryProvider
    {
        private readonly DbContextBase context;

        public DefaultRepositoryProvider(DbContextBase context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.context = context;
        }

        public IRepository<TEntity> GetRepositoryForEntityType<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(this.context);
        }
    }
}
