using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Specifications
{
    public class OrSpecification<T> : CompositeSpecfication<T>
        where T : class
    {
        // Constructors
        public OrSpecification(ISpecification<T> spec1, ISpecification<T> spec2)
            : base(spec1, spec2)
        {
        }

        // Overriden Methods
        public override bool IsSatisfiedBy(T entity)
        {
            return Spec1.IsSatisfiedBy(entity) || Spec2.IsSatisfiedBy(entity);
        }
    }
}
