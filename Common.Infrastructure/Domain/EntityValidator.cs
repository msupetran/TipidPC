using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Domain
{
    public class EntityValidator<TEntity> : IEntityValidator<TEntity>
        where TEntity : class
    {
        public IEnumerable<ValidationResult> GetValidationErrors(TEntity obj)
        {
            /*
            TypeDescriptor.AddProviderTransparent(
            new AssociatedMetadataTypeTypeDescriptionProvider(typeof(Persona), typeof(Persona_Validation)), typeof(Persona));
            */


            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(obj, null, null);
            Validator.TryValidateObject(obj, validationContext, validationResults, true);
            return validationResults;
        }
        public bool IsValid(TEntity entity)
        {
            return (GetValidationErrors(entity).Count() == 0);
        }
    }
}
