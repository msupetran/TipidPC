using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Domain
{
    public interface IEntityValidator<TEntity> 
        where TEntity : class
    {
        IEnumerable<ValidationResult> GetValidationErrors(TEntity entity);
        bool IsValid(TEntity entity);
    }
}
