using Common.Infrastructure.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TipidPC.Domain.Models;
using Common.Infrastructure.Specification;

namespace TipidPC.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        // Fields
        private bool disposed = false;
        private ITipidPcContext _context;
        private IDictionary _repositoryDictionary;
        
        // Constructors
        public UnitOfWork(ITipidPcContext context)
        {
            _context = context;
        }

        // Methods
        public int Save()
        {
            return _context.SaveChanges();
        }
        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            string entityName = typeof(TEntity).ToString();
            IRepository<TEntity> repository = null;

            if (_repositoryDictionary == null)
            {
                _repositoryDictionary = new Dictionary<string, object>();
            }

            if (!_repositoryDictionary.Contains(entityName))
            {
                repository = new GenericRepository<TEntity>(_context);
                _repositoryDictionary.Add(entityName, repository);

            }
            
            return repository ?? (IRepository<TEntity>)_repositoryDictionary[entityName];

        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
