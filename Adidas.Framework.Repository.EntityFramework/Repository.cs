namespace Adidas.Framework.Repository.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Linq.Expressions;

    using Adidas.Framework.Repository.Repositories;

    public sealed class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IDbSet<TEntity> dbSet;

        public Repository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.dbSet = context.Set<TEntity>();
        }

        public TEntity Find(params object[] ids)
        {
            return this.dbSet.Find(ids);
        }

        public void Add(TEntity entity)
        {
            this.dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                this.Add(entity);
            }
        }

        public void Update(TEntity entity)
        {
            this.dbSet.AddOrUpdate(entity);
        }

        public void Delete(params object[] ids)
        {
            var entity = this.dbSet.Find(ids);
            this.Delete(entity);
        }

        public void Delete(TEntity entity)
        {
            this.dbSet.Remove(entity);
        }

        public IRepositoryQuery<TEntity> Query()
        {
            return new RepositoryQuery<TEntity>(this);
        }

        internal IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includeProperties = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = this.dbSet;

            if (includeProperties != null)
            {
                includeProperties.ForEach(i => query = query.Include(i));
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (page != null && pageSize != null)
            {
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            var results = query;

            return results;
        }
    }
}
