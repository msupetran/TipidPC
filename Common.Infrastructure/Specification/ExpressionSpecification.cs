using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public ExpressionSpecification() : this(t => true) { }
        public ExpressionSpecification(Expression<Func<T, bool>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                this.IsMatchByExpression = expression;
                _expression = expression.Compile();
            }
        }

        // Methods
        public override bool IsMatch(T o)
        {
            return _expression(o);
        }
    }
}
