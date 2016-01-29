using Common.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Data
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
        IRepository<TEntity> GetRepository<TEntity, TRepository>(params object[] args) 
            where TEntity : class
            where TRepository : IRepository<TEntity>;
        IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class;
    }
}
