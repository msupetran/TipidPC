using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.AspNet.Identity.EntityFramework
{
    public class IdentityDbContextIntPk<TUser> : IdentityDbContext<TUser, RoleIntPk, int, UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
        where TUser : IdentityUser<int, UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        // Constructors
        public IdentityDbContextIntPk(string connection) : base(connection) { }

        // Overridden methods from IdentityDbContext
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<TUser>().ToTable("User");
            modelBuilder.Entity<RoleIntPk>().ToTable("Role");
            modelBuilder.Entity<UserClaimIntPk>().ToTable("UserClaim");
            modelBuilder.Entity<UserLoginIntPk>().ToTable("UserLogin");
            modelBuilder.Entity<UserRoleIntPk>().ToTable("UserRole");
        }
    }
}
