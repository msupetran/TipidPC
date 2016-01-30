using Common.Infrastructure.Domain;
using TipidPc.Infrastructure.Domain;

namespace TipidPc.Infrastructure.Data
{
    public class TpcUnitOfWork : UnitOfWorkBase<ITpcContext>
    {
        // Constructors
        public TpcUnitOfWork(ITpcContext context) : base(context) { }

        // Methods implemented from UnitOfWorkBase<T>
        public override IRepository<TEntity> GetRepository<TEntity>()
        {
            return new TpcRepository<TEntity>(this.Context);
        }
    }
}
