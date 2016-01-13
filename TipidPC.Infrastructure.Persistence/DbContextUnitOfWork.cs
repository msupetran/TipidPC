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
    public class DbContextUnitOfWork : DbContext, IUnitOfWork
    {
        // Properties
        public DbSet<User> Users { get; set; }
        public DbSet<Registration> Registration { get; set; }

        // Constructors
        public DbContextUnitOfWork() : base("DefaultConnection")
        {
        }

        // Methods
        public int Save()
        {
            throw new NotImplementedException();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
