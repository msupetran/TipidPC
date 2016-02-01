using Common.Infrastructure.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Domain
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        TEntity Select(object id);
        IEnumerable<TEntity> Select();
        IEnumerable<TEntity> Select(ISpecification<TEntity> spec);
        IEnumerable<TEntity> Select(ISpecification<TEntity> spec, params Expression<Func<TEntity, object>>[] paths);
        TEntity Insert(TEntity entity);
        void Update(TEntity entity);
        void Update(TEntity entity, params Expression<Func<TEntity, object>>[] paths);
        void Delete(object id);
        void Delete(TEntity entity);
    }
}
