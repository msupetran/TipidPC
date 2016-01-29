using Common.Infrastructure.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Common.Infrastructure.Domain;
using Common.Infrastructure.Data;

namespace Common.Infrastructure.Domain
{
    public abstract class RepositoryBase<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class
        where TContext : IContext
    {
        // Fields
        private TContext _context;

        // Properties
        protected TContext Context
        {
            get { return _context; }
        }

        // Constructors
        protected RepositoryBase(TContext context)
        {
            _context = context;
        }

        // Methods
        public abstract TEntity Insert(TEntity newEntity);
        public abstract TEntity Select(object id);
        public abstract IEnumerable<TEntity> Select();
        public abstract IEnumerable<TEntity> Select(ISpecification<TEntity> spec);
        public abstract IEnumerable<TEntity> Select(ISpecification<TEntity> spec, params Expression<Func<TEntity, object>>[] paths);
        public abstract void Update(TEntity entity);
        public abstract void Delete(object id);
        public abstract void Delete(TEntity item);
    }
}
