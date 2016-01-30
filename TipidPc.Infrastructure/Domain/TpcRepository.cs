using Common.Infrastructure.Domain;
using TipidPc.Infrastructure.Data;

namespace TipidPc.Infrastructure.Domain
{
    public class TpcRepository<TEntity> : EntityFrameworkRepository<TEntity> //RepositoryBase<TEntity, ITpcContext>
            where TEntity : class
    {
        // Constructors
        public TpcRepository(ITpcContext context) : base(context) { }
    }
}
