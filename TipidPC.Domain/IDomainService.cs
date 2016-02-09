using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TipidPc.Domain.Models;

namespace TipidPc.Domain
{
    public interface IDomainService<TEntity>
        where TEntity : class
    {
        // Properties
        List<ValidationResult> ValidationErrors { get; }

        // MEthods
        void Add(TEntity item);
        TEntity QueryById(int id);
        IEnumerable<TEntity> Query();
        IEnumerable<TEntity> QueryByUserId(int userId);
        void Update(TEntity newItem);
    }
}