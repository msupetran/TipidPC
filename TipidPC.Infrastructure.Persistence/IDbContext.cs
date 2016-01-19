using Common.Infrastructure.AspNet.Identity.EntityFramework;
using Common.Infrastructure.Persistence;
using Microsoft.AspNet.Identity.EntityFramework;
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
    public interface IDbContext : IDisposable
    {
        // Propestires
        DbSet<Bookmark> Bookmarks { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Entry> Entries { get; set; }
        DbSet<Header> Headers { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<Location> Locations { get; set; }
        DbSet<Rating> Ratings { get; set; }
        DbSet<Section> Sections { get; set; }
        DbSet<Topic> Topics { get; set; }

        // Methods
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
    }

    //public interface ITipidPcContext<T> : IDisposable
    //    where T : class
    //{
    //    // Propestires
    //    DbSet<Item> Items { get; set; }
    //    DbSet<Header> Headers { get; set; }
    //    DbSet<Entry> Entries { get; set; }

    //    // Methods
    //    DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    //    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    //    int SaveChanges();
    //}
}
