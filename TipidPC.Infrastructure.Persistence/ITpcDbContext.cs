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
    public interface ITpcDbContext : IDisposable
    {
        // Propestires
        DbSet<User> Users { get; set; }
        DbSet<Registration> Registration { get; set; }

        // Methods
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
    }

    public interface ITpcDbContext<T> : IDisposable
        where T : class
    {
        // Propestires
        DbSet<T> Users { get; set; }

        // Methods
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
    }
}
