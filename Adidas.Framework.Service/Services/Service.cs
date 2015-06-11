namespace Adidas.Framework.Service.Services
{
    using System;
    using System.Collections.Generic;

    using Adidas.Framework.Repository.Repositories;
    using Adidas.Framework.Repository.UnitOfWork;

    /// <summary>
    /// The base class for Service.
    /// </summary>
    public abstract class Service
    {
        private readonly IUnitOfWork unitOfWork;

        protected Service(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            this.unitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork
        {
            get { return this.unitOfWork; }
        }
    }

    /// <summary>
    /// The base class for Service parameterized by Entity.
    /// </summary>
    /// <typeparam name="TEntity">DB context entity</typeparam>
    public abstract class Service<TEntity> : Service, IService<TEntity> where TEntity : class
    {
        protected Service(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        public TEntity FindById(params object[] ids)
        {
            return base.UnitOfWork.Repository<TEntity>().Find(ids);
        }

        public void Add(TEntity entity)
        {
            base.UnitOfWork.Repository<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            base.UnitOfWork.Repository<TEntity>().AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            base.UnitOfWork.Repository<TEntity>().Update(entity);
        }

        public void Delete(params object[] ids)
        {
            base.UnitOfWork.Repository<TEntity>().Delete(ids);
        }

        public void Delete(TEntity entity)
        {
            base.UnitOfWork.Repository<TEntity>().Delete(entity);
        }

        public IRepositoryQuery<TEntity> Query()
        {
            return base.UnitOfWork.Repository<TEntity>().Query();
        }
    }
}
