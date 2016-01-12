﻿using Common.Infrastructure.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipidPC.Domain
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        TEntity Select(object id);
        IEnumerable<TEntity> Select();
        IEnumerable<TEntity> Select(ISpecification<TEntity> spec);
        TEntity Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
    }
}