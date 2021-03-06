﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Specification
{
    public abstract class CompositeSpecfication<T>
        : ISpecification<T>
        where T : class
    {
        // Fields
        //private ISpecification<T> _spec1;
        //private ISpecification<T> _spec2;
        private Expression<Func<T, bool>> _expression;

        // Properties
        //protected ISpecification<T> Spec1
        //{
        //    get { return _spec1; }
        //}
        //protected ISpecification<T> Spec2
        //{
        //    get { return _spec2; }
        //}
        public Expression<Func<T, bool>> Expression
        {
            get { return _expression; }
            protected set { _expression = value; }
        }

        // Constructors
        //protected CompositeSpecfication(ISpecification<T> spec1, ISpecification<T> spec2)
        //{
        //    _spec1 = spec1;
        //    _spec2 = spec2;
        //}
        //protected CompositeSpecfication()
        //{
        //}

        // Methods
        //public abstract bool IsMatch(T entity);
    }
}
