using Common.Infrastructure.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TipidPc.Domain.Models;
using TipidPc.Infrastructure.Data;

namespace TipidPc.Infrastructure.Data
{
    public class TpcContext : DbContext, ITpcContext
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
        public TpcContext() : this("DefaultConnection") { }
        public TpcContext(string connection) : base(connection) { }

        // Methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
