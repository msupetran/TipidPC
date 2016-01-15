using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace TipidPC.Presentation.Web.Identity
{
    public class TpcUserManager<TUser> : UserManager<TUser, int> where TUser : class, IUser<int>
    {
        public TpcUserManager(IUserStore<TUser, int> store) : base(store)
        {
        }

        override crea
    }
}