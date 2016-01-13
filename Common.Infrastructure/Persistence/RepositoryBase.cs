using Common.Infrastructure.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Persistence
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        // Methods
        public abstract TEntity Insert(TEntity newEntity);
        public abstract TEntity Select(object id);
        public abstract IEnumerable<TEntity> Select();
        public abstract IEnumerable<TEntity> Select(ISpecification<TEntity> spec);
        public abstract void Update(TEntity item);
        public abstract void Delete(object id);
        public abstract void Delete(TEntity item);
    }
}

