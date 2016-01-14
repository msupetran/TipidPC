using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TipidPC.Domain.Models;
using System.Threading.Tasks;
using TipidPC.Infrastructure.Persistence;
using System.Security.Claims;
using TipidPC.Presentation.Web.Identity;
using System.Data.Entity;

namespace TipidPC.Presentation.Web.Models
{
    public class TpcApplicationUser : User, IUser<int>
    {
        // Constructors
        public TpcApplicationUser() { }

        // Methods
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(TpcUserManager<TpcApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class TpcApplicationDbContext : TpcDbContext<TpcApplicationUser>
    {
        // Ctor
        public TpcApplicationDbContext() : base("DefaultConnection") { }

        // Methods
        public static TpcApplicationDbContext Create()
        {
            return new TpcApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TpcApplicationUser>().ToTable("User");       
            //modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            //modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            //modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            //modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
        }
    }
}