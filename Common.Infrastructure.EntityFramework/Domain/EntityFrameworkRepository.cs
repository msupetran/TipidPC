using Common.Infrastructure.Data;
using Common.Infrastructure.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Common.Infrastructure.Domain
{
    public abstract class EntityFrameworkRepository<TEntity> : RepositoryBase<TEntity, IDbContext>
            where TEntity : class
    {
        // Fields
        //private ITpcContext _ccontext;

        //// Properties
        //public ITpcContext TpcContext
        //{
        //    get { return _ccontext; }
        //}

        // Constructors
        public EntityFrameworkRepository(IDbContext context) : base(context)
        {
        }

        // Methods
        public override TEntity Insert(TEntity newEntity)
        {
            this.Context.Set<TEntity>().Add(newEntity);
            //_context.SaveChanges();
            return newEntity;
        }
        public override TEntity Select(object id)
        {
            return this.Context.Set<TEntity>().Find(id);
        }
        public override IEnumerable<TEntity> Select()
        {
            return this.Context.Set<TEntity>()
                .ToList();
        }
        public override IEnumerable<TEntity> Select(ISpecification<TEntity> spec)
        {
            return this.Context
                .Set<TEntity>()
                .Where(spec.Expression)
                .ToList();
        }
        public override IEnumerable<TEntity> Select(ISpecification<TEntity> spec, params Expression<Func<TEntity, object>>[] included)
        {
            var entities = this.Context
                .Set<TEntity>()
                .Include(included.First());

            for (int i = 1; i < included.Length; i++)
            {
                entities = entities.Include(included[i]);
            }

            return entities
                .Where(spec.Expression)
                .ToList();
        }
        public override void Update(TEntity entity)
        {
            if (this.Context.Entry(entity).State == EntityState.Detached)
            {
                this.Context.Set<TEntity>().Attach(entity);
            }
            this.Context.Entry(entity).State = EntityState.Modified;
        }
        public override void Update(TEntity entity, params Expression<Func<TEntity, object>>[] excludedProperties)
        {
            if (this.Context.Entry(entity).State == EntityState.Detached)
            {
                this.Context.Set<TEntity>().Attach(entity);

                var entry = this.Context.Entry(entity);
                var databaseValues = entry.GetDatabaseValues();

                foreach (var propertyName in databaseValues.PropertyNames)
                {
                    entry.Property(propertyName).IsModified = true;
                }
                
                foreach (var property in excludedProperties)
                {
                    entry.Property(property).IsModified = false;
                }
            }
        }
        public override void Delete(object id)
        {
            TEntity itemToDelete = this.Context.Set<TEntity>().Find(id);
            this.Delete(itemToDelete);
        }
        public override void Delete(TEntity item)
        {
            if (this.Context.Entry(item).State == EntityState.Detached)
            {
                this.Context.Set<TEntity>().Attach(item);
            }
            this.Context.Set<TEntity>().Remove(item);
        }
    }
}
