using Common.Infrastructure.AspNet.Identity.EntityFramework;
using Common.Infrastructure.Persistence;
using Microsoft.AspNet.Identity.EntityFramework;
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
    public class DbContext : System.Data.Entity.DbContext, IDbContext
    {
        // Properties
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Header> Headers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Topic> Topics { get; set; }

        // Constructors
        public DbContext() : this("DefaultConnection") { }
        public DbContext(string connection) : base(connection) { }

        // Methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public class DbContextIntPk<TUser> : IdentityDbContextIntPk<TUser>, IDbContext
        where TUser : IdentityUser<int, UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        // Properties
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Header> Headers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Topic> Topics { get; set; }

        // Constructors
        public DbContextIntPk() : this("DefaultConnection") { }
        public DbContextIntPk(string connection) : base(connection) { }

        // Methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<RoleIntPk>().ToTable("Role");
            modelBuilder.Entity<UserClaimIntPk>().ToTable("UserClaim");
            modelBuilder.Entity<UserLoginIntPk>().ToTable("UserLogin");
            modelBuilder.Entity<UserRoleIntPk>().ToTable("UserRole");
        }
    }

    //public class TipidPcContext<TEntity> : DbContext, ITipidPcContext<TEntity>
    //    where TEntity : class
    //{
    //    // Properties
    //    public DbSet<Item> Items { get; set; }
    //    public DbSet<Header> Headers { get; set; }
    //    public DbSet<Entry> Entries { get; set; }

    //    // Constructors
    //    public TipidPcContext(string connection) : base(connection) { }
    //}
}
