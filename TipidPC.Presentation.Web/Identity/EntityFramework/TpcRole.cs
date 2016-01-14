using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TipidPC.Presentation.Web.Identity.EntityFramework
{
    public class TpcRole : IdentityRole<int, TpcUserRole>
    {
        public TpcRole() { }
        public TpcRole(string name)
        {
            Name = name;
        }
    }
}