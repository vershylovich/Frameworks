namespace Adidas.Framework.Repository.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Adidas.Framework.Repository.Repositories;

    public sealed class RepositoryQuery<TEntity> : IRepositoryQuery<TEntity> where TEntity : class
    {
        private readonly Repository<TEntity> repository;
        private readonly List<Expression<Func<TEntity, object>>> includeProperties;
        private Expression<Func<TEntity, bool>> filter;
        private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy;
        private int? page;
        private int? pageSize;

        public RepositoryQuery(Repository<TEntity> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.repository = repository;
            this.includeProperties = new List<Expression<Func<TEntity, object>>>();
        }

        public IRepositoryQuery<TEntity> Filter(Expression<Func<TEntity, bool>> filter)
        {
            this.filter = filter;
            return this;
        }

        public IRepositoryQuery<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            this.orderBy = orderBy;
            return this;
        }

        public IRepositoryQuery<TEntity> Include(Expression<Func<TEntity, object>> expression)
        {
            this.includeProperties.Add(expression);
            return this;
        }

        public IEnumerable<TEntity> GetPage(int page, int pageSize, out int totalCount)
        {
            this.page = page;
            this.pageSize = pageSize;
            totalCount = this.repository.Get(this.filter).Count();

            return this.repository.Get(
                this.filter, this.orderBy, this.includeProperties, this.page, this.pageSize);
        }

        public IQueryable<TEntity> Get()
        {
            return this.repository.Get(
                this.filter, this.orderBy, this.includeProperties, this.page, this.pageSize);
        }
    }
}
