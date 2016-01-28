using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Infrastructure.Linq.Expressions;

namespace Common.Infrastructure.Specification
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
        public NotSpecification(ISpecification<T> spec1, ISpecification<T> spec2)
            : base(spec1, spec2)
        {
            this.Expression = spec1.Expression.Or<T>(spec2.Expression);
        }

        // Overriden Methods
        //public override bool IsMatch(T o)
        //{
        //    return !Spec.IsMatchByExpression.Compile()(o);
        //}
    }
}
