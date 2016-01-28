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
        private bool _disposed = false;
        private ITpcContext _context;
        private Dictionary<Type, object> _repositoryDictionary;

        // Constructors
        public UnitOfWork(ITpcContext context)
        {
            _context = context;
        }

        // IUnitOfWork Methods
        public int Commit()
        {
            return _context.SaveChanges();
        }
        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            return this.GetRepository<TEntity, GenericRepository<TEntity>>(_context);
        }
        public IRepository<TEntity> GetRepository<TEntity, TRepository>(params object[] args)
            where TEntity : class
            where TRepository : IRepository<TEntity>
        {
            Type entityType = typeof(TEntity);
            Type repositoryType = typeof(TRepository);

            // Check if dictionary has an instance, else instantiate...
            if (_repositoryDictionary == null)
            {
                _repositoryDictionary = new Dictionary<Type, object>();
            }

            // Check if repository already exists in dictionary, else create one and add it to the dictionary...
            IRepository<TEntity> repository = null;
            if (_repositoryDictionary.ContainsKey(entityType))
                repository = (IRepository<TEntity>)_repositoryDictionary[entityType];
            else
                _repositoryDictionary.Add(
                    entityType,
                    repository = (TRepository)Activator.CreateInstance(repositoryType, args));
            
            return repository;
        }

        // Methods
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Release all disposable objects here...
                _context.Dispose();
            }

            // Release non-disposable objects here by setting the reference to null...
            _repositoryDictionary = null;

            _disposed = true;
        }
        public void Dispose()
        {
            
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
