using Common.Infrastructure.Data;
using Common.Infrastructure.Domain;
using Common.Infrastructure.Specification;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TipidPC.Infrastructure.Persistence
{
    public class TpcRepositoryBase<TEntity> : RepositoryBase<TEntity>
            where TEntity : class
    {
        // Fields
        private ITpcContext _tpcCcontext;

        // Properties
        public ITpcContext TpcContext
        {
            get { return _tpcCcontext; }
        }

        // Constructors
        public TpcRepositoryBase(ITpcContext TpcContext) : base(TpcContext)
        {
            _tpcCcontext = TpcContext;
        }

        // Methods
        public override TEntity Insert(TEntity newEntity)
        {
            this.TpcContext.Set<TEntity>().Add(newEntity);
            //_context.SaveChanges();
            return newEntity;
        }
        public override TEntity Select(object id)
        {
            return _tpcCcontext.Set<TEntity>().Find(id);
        }
        public override IEnumerable<TEntity> Select()
        {
            return _tpcCcontext.Set<TEntity>()
                .ToList();
        }
        public override IEnumerable<TEntity> Select(ISpecification<TEntity> spec)
        {
            return _tpcCcontext
                .Set<TEntity>()
                .Where(spec.Expression)
                .ToList();
        }
        public override IEnumerable<TEntity> Select(ISpecification<TEntity> spec, params Expression<Func<TEntity, object>>[] paths)
        {
            var entities = _tpcCcontext
                .Set<TEntity>()
                .Include(paths.First());

            for (int i = 1; i < paths.Length; i++)
            {
                entities = entities.Include(paths[i]);
            }

            return entities
                .Where(spec.Expression)
                .ToList();
        }
        public override void Update(TEntity entity)
        {
            if (_tpcCcontext.Entry(entity).State == EntityState.Detached)
            {
                _tpcCcontext.Set<TEntity>().Attach(entity);
            }
            _tpcCcontext.Entry(entity).State = EntityState.Modified;
        }
        public override void Delete(object id)
        {
            TEntity itemToDelete = _tpcCcontext.Set<TEntity>().Find(id);
            this.Delete(itemToDelete);
        }
        public override void Delete(TEntity item)
        {
            if (_tpcCcontext.Entry(item).State == EntityState.Detached)
            {
                _tpcCcontext.Set<TEntity>().Attach(item);
            }
            _tpcCcontext.Set<TEntity>().Remove(item);
        }
    }
}
