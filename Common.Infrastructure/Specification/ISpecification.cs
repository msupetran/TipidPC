using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Specification
{
    public interface ISpecification<T>
        where T : class
    {
        bool IsMatch(T entity);
    }
}
