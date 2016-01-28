﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
        public NotSpecification(ISpecification<T> specification)
        {
            _spec = specification;
        }

        // Overriden Methods
        public override bool IsMatch(T o)
        {
            return !Spec.IsMatchByExpression.Compile()(o);
        }
    }
}
