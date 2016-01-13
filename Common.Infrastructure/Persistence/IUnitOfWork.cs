using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();
    }
}
