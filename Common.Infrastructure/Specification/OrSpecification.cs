using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Common.Infrastructure.Linq.Expressions;

namespace Common.Infrastructure.Specification
{
    public class OrSpecification<T> : CompositeSpecfication<T>
        where T : class
    {
        // Constructors
        public OrSpecification(ISpecification<T> spec1, ISpecification<T> spec2)
            : base(spec1, spec2)
        {
            this.Expression = spec1.Expression.Or(spec2.Expression);
        }

        // Overriden Methods
        //public override bool IsMatch(T entity)
        //{
        //    return Spec1.IsMatchByExpression.Compile()(entity) || Spec2.IsMatchByExpression.Compile()(entity);
        //}
    }
}
