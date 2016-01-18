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

namespace TipidPC.Infrastructure.Persistence
{
    public abstract class DbContextRepository<TEntity> : 
        RepositoryBase<TEntity>
        where TEntity : class
    {
        // Fields
        private ITipidPcContext _context;

        // Constructors
        protected DbContextRepository(ITipidPcContext context)
        {
            _context = context;
        }

        // Methods
        public override TEntity Insert(TEntity newEntity)
        {
            _context.Set<TEntity>().Add(newEntity);
            _context.SaveChanges();
            return newEntity;
        }
        public override TEntity Select(object id)
        {
            return _context.Set<TEntity>().Find(id);
        }
        public override IEnumerable<TEntity> Select()
        {
            return _context.Set<TEntity>()
                .ToList();
        }
        public override IEnumerable<TEntity> Select(ISpecification<TEntity> spec)
        {
            return _context.Set<TEntity>()
                .Where(spec.IsSatisfiedBy)
                .ToList();
        }
        public override void Update(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Set<TEntity>().Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;
        }
        public override void Delete(object id)
        {
            TEntity itemToDelete = _context.Set<TEntity>().Find(id);
            this.Delete(itemToDelete);
        }
        public override void Delete(TEntity item)
        {
            if (_context.Entry(item).State == EntityState.Detached)
            {
                _context.Set<TEntity>().Attach(item);
            }
            _context.Set<TEntity>().Remove(item);
        }
    }
}
