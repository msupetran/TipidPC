using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Specification
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
        public override bool IsMatch(T entity)
        {
            return Spec1.IsMatchByExpression.Compile()(entity) || Spec2.IsMatchByExpression.Compile()(entity);
        }
    }
}
