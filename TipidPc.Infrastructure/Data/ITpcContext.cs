using Common.Infrastructure.AspNet.Identity.EntityFramework;
using Common.Infrastructure.Data;
using Common.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TipidPc.Domain.Models;

namespace TipidPc.Infrastructure.Data
{
    public interface ITpcContext : IDbContext
    {
        // Properties
        DbSet<Bookmark> Bookmarks { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Entry> Entries { get; set; }
        DbSet<Header> Headers { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<Location> Locations { get; set; }
        DbSet<Rating> Ratings { get; set; }
        DbSet<Section> Sections { get; set; }
        DbSet<Topic> Topics { get; set; }
    }
}
