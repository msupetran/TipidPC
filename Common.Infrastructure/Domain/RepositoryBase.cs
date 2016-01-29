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
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        // Fields
        private IContext _context;

        // Properties
        protected IContext Context
        {
            get { return _context; }
        }

        // Constructors
        protected RepositoryBase(IContext context)
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

    //public class GenericRepository<TEntity> : RepositoryBase<TEntity>
    //    where TEntity : class
    //{
    //    public GenericRepository(IContext context) : base(context) { }
    //}
}
