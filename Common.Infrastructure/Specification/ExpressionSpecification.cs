using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Specification
{
    public class ExpressionSpecification<T> :
        CompositeSpecfication<T>
        where T : class
    {
        // Fields
        private Func<T, bool> _expression;

        // Constructors
        public ExpressionSpecification() : this(new Func<T, bool>(t => true)) { }
        public ExpressionSpecification(Func<T,bool> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                _expression = expression;
            }
        }

        // Methods
        public override bool IsSatisfiedBy(T o)
        {
            return _expression(o);
        }
    }
}
