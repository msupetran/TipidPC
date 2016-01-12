using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Specifications
{
    public static class SpecificationExtensionMethods
    {
        public static ISpecification<T> And<T>(this ISpecification<T> spec1, ISpecification<T> spec2)
            where T : class
        {
            return new AndSpecification<T>(spec1, spec2);
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
        public static ISpecification<T> And<T>(this ISpecification<T> spec, Func<T,bool> expression)
            where T : class
        {
            return spec.And<T>(new ExpressionSpecification<T>(expression));
        }
        public static ISpecification<T> Or<T>(this ISpecification<T> spec, Func<T, bool> expression)
            where T : class
        {
            return spec.Or<T>(new ExpressionSpecification<T>(expression));
        }
        public static ISpecification<T> Not<T>(this ISpecification<T> expressionSpec, Func<T, bool> expression)
            where T : class
        {
            return expressionSpec.And(new ExpressionSpecification<T>(expression).Not());
        }
    }
}
