using Common.Infrastructure.Persistence;
using Common.Infrastructure.Specification;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TipidPC.Domain;
using TipidPC.Domain.Models;
using System.Linq.Expressions;

namespace TipidPC.Infrastructure.Persistence
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        // Fields
        private ITpcContext _context;

        public ITpcContext Context
        {
            get
            {
                return _context;
            }

            set
            {
                _context = value;
            }
        }

        // Constructors
        public RepositoryBase(ITpcContext context)
        {
            _context = context;
        }

        // Methods
        public TEntity Insert(TEntity newEntity)
        {
            _context.Set<TEntity>().Add(newEntity);
            //_context.SaveChanges();
            return newEntity;
        }
        public TEntity Select(object id)
        {
            return _context.Set<TEntity>().Find(id);
        }
        public IEnumerable<TEntity> Select()
        {
            return _context.Set<TEntity>()
                .ToList();
        }
        public IEnumerable<TEntity> Select(ISpecification<TEntity> spec)
        {
            return _context
                .Set<TEntity>()
                .Where(spec.IsMatchByExpression)
                .ToList();
        }
        public IEnumerable<TEntity> Select(ISpecification<TEntity> spec, params Expression<Func<TEntity, object>>[] paths)
        {
            var entities = _context
                .Set<TEntity>()
                .Include(paths.First());

            for (int i = 1; i < paths.Length; i++)
            {
                entities = entities.Include(paths[i]);
            }

            return entities
                .Where(spec.IsMatchByExpression)
                .ToList();
        }
        public void Update(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Set<TEntity>().Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            TEntity itemToDelete = _context.Set<TEntity>().Find(id);
            this.Delete(itemToDelete);
        }
        public void Delete(TEntity item)
        {
            if (_context.Entry(item).State == EntityState.Detached)
            {
                _context.Set<TEntity>().Attach(item);
            }
            _context.Set<TEntity>().Remove(item);
        }
    }

    public class GenericRepository<TEntity> : RepositoryBase<TEntity>
        where TEntity : class
    {
        public GenericRepository(ITpcContext context) : base(context) { }
    }
}
