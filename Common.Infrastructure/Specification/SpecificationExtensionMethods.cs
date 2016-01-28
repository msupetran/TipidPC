using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Specification
{
    public static class SpecificationExtensionMethods
    {
        public static ISpecification<T> And<T>(this ISpecification<T> spec1, ISpecification<T> spec2)
            where T : class
        {
            var spec = new AndSpecification<T>(spec1, spec2);
            return spec;
        }
        public static ISpecification<T> Or<T>(this ISpecification<T> spec1, ISpecification<T> spec2)
            where T : class
        {
            return new OrSpecification<T>(spec1, spec2);
        }
        public static ISpecification<T> Not<T>(this ISpecification<T> spec)
            where T : class
        {
            return new NotSpecification<T>(spec);
        }
        public static ISpecification<T> And<T>(this ISpecification<T> spec, Expression<Func<T, bool>> expression)
            where T : class
        {
            var spec2 = new ExpressionSpecification<T>(expression);
            return spec.And<T>(spec2);
        }
        public static ISpecification<T> Or<T>(this ISpecification<T> spec, Expression<Func<T, bool>> expression)
            where T : class
        {
            return spec.Or<T>(new ExpressionSpecification<T>(expression));
        }
        public static ISpecification<T> Not<T>(this ISpecification<T> expressionSpec, Expression<Func<T, bool>> expression)
            where T : class
        {
            return expressionSpec.And(new ExpressionSpecification<T>(expression).Not());
        }
    }
}
