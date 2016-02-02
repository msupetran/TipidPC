using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Data
{
    public abstract class EntityFrameworkDbContext : DbContext, IDbContext, IContext
    {
        public EntityFrameworkDbContext(string connection) : base(connection) { }
    }
}
