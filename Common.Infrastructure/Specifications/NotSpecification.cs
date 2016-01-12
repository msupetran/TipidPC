using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Specifications
{
    public class NotSpecification<T> : CompositeSpecfication<T>
        where T : class
    {
        // Fields
        private ISpecification<T> _spec;

        // Properties
        public ISpecification<T> Spec
        {
            get
            {
                return _spec;
            }
        }

        // Constructors
        public NotSpecification(ISpecification<T> specification)
        {
            _spec = specification;
        }

        // Overriden Methods
        public override bool IsSatisfiedBy(T o)
        {
            return !Spec.IsSatisfiedBy(o);
        }
    }
}
