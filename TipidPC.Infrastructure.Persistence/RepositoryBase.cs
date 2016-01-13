using Common.Infrastructure.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TipidPC.Domain;

namespace TipidPC.Infrastructure.Persistence
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        // Fields
        private IOnlineStoreDBContext _context;

        // Constructors
        protected RepositoryBase(IOnlineStoreDBContext context)
        {
            _context = context;
        }

        // Methods
        public TEntity Insert(TEntity newEntity)
        {
            _context.Set<TEntity>().Add(newEntity);
            _context.SaveChanges();
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
            return _context.Set<TEntity>()
                .Where(spec.IsSatisfiedBy)
                .ToList();
        }
        public void Update(TEntity item)
        {
            if (_context.Entry(item).State == EntityState.Detached)
            {
                _context.Set<TEntity>().Attach(item);
            }
            _context.Entry(item).State = EntityState.Modified;
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
}
}
