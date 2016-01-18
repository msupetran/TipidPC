﻿using Common.Infrastructure.AspNet.Identity.EntityFramework;
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
    public class TipidPcContext : DbContext, ITipidPcContext
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
        public TipidPcContext() : this("DefaultConnection") { }
        public TipidPcContext(string connection) : base(connection) { }

        // Methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public class TipidPcAspNetDbContext<TUser> : IdentityDbContextIntPk<TUser>, ITipidPcContext
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
        public TipidPcAspNetDbContext() : this("DefaultConnection") { }
        public TipidPcAspNetDbContext(string connection) : base(connection) { }

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
