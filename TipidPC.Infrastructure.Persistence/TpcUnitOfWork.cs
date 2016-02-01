using Common.Infrastructure.Domain;

namespace TipidPC.Infrastructure.Persistence
{
    public class TpcUnitOfWork : UnitOfWorkBase<ITpcContext>
    {
        // Constructors
        public TpcUnitOfWork(ITpcContext context) : base(context) { }

        // Methods
        public override IRepository<TEntity> GetRepository<TEntity>()
        {
            return this.GetRepository<TEntity, TpcRepository<TEntity>>(this.Context);
        }
    }
}
