using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Data
{
    public interface IContext : IDisposable
    {
        int SaveChanges();
    }
}
