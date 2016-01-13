using Common.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TipidPC.Domain.Models;

namespace TipidPC.Infrastructure.Persistence
{
    public interface IDbContextUnitOfWork : IUnitOfWork
    {
        // Propestires
        DbSet<User> Users { get; set; }
        DbSet<Registration> Registration { get; set; }

        // Methods
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
