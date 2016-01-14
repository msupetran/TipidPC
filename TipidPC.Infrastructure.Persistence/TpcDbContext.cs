using Common.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TipidPC.Domain.Models;

namespace TipidPC.Infrastructure.Persistence
{
    public class TpcDbContext : DbContext, ITpcDbContext
    {
        // Properties
        public DbSet<User> Users { get; set; }
        public DbSet<Registration> Registration { get; set; }

        // Constructors
        public TpcDbContext() : this("DefaultConnection") { }
        public TpcDbContext(string connection) : base(connection) { }

        // Methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public int Save()
        {
            return this.SaveChanges();
        }
    }

    public class TpcDbContext<TEntity> : DbContext, ITpcDbContext<TEntity>
        where TEntity : class
    {
        // Properties
        public DbSet<TEntity> Users { get; set; }

        // Constructors
        public TpcDbContext(string connection) : base(connection) { }
    }
}
