using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.AspNet.Identity.EntityFramework
{
    public class IdentityDbContextIntPk<TUser> : IdentityDbContext<TUser, RoleIntPk, int, UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
        where TUser : IdentityUser<int, UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public IdentityDbContextIntPk(string connection) : base(connection) { }
    }
}
