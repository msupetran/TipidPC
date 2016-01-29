using Common.Infrastructure.Domain;
using Common.Infrastructure.Specification;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace TipidPC.Infrastructure.Persistence
{
    public class TpcRepository<TEntity> : EntityFrameworkRepository<TEntity> //RepositoryBase<TEntity, ITpcContext>
            where TEntity : class
    {
        // Constructors
        public TpcRepository(ITpcContext context) : base(context) { }
    }
}
