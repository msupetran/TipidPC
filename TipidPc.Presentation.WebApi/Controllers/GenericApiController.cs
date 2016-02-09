using Common.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TipidPc.Domain;
using TipidPc.Presentation.WebApi.Models;
using TipidPc.Domain.Models;
using System.IO;
using Common.Infrastructure.Data;
using TipidPc.Infrastructure.Data;

namespace TipidPc.Presentation.WebApi.Controllers
{
    public abstract class GenericApiController<TEntity, TDomainService, TViewModel> : ApiController
        where TEntity : class
        where TDomainService : IDomainService<TEntity>
        where TViewModel : IViewModel
    {
        // Fields
        protected ITpcContext _context;
        protected IUnitOfWork _uow;
        protected IDomainService<TEntity> _domainService;

        // Constructor
        public GenericApiController()
        {
        }

        // Methods
        public virtual IHttpActionResult Get(int id = 0)
        {
            try
            {
                if (id <= 0)
                {
                    var vms = this.GetViewModels(_domainService.Query());
                    if (vms != null && vms.Count() > 0)
                    {
                        return Ok(vms);
                    }
                }

                var vm = this.GetViewModel(_domainService.QueryById(id));
                if (vm != null)
                {
                    return Ok(vm);
                }

                // else...
                return NotFound();
            }
            catch (Exception ex)
            {
                // TODO: Log errors...
                return InternalServerError(ex);
            }
        }
        public virtual IHttpActionResult Post([FromBody]TViewModel vm)
        {
            try
            {
                if (vm == null)
                {
                    return BadRequest();
                }

                // do further validation...
                var entity = this.GetEntity(vm);
                _domainService.Add(entity);
                _uow.Commit();
                
                return Created<TViewModel>(this.GetLocation(entity), vm);
            }
            catch (Exception ex)
            {
                // log exception...
                return InternalServerError();
            }
        }
        protected abstract TViewModel GetViewModel(TEntity entity);
        protected abstract IEnumerable<TViewModel> GetViewModels(IEnumerable<TEntity> entities);
        protected abstract TEntity GetEntity(TViewModel viewModel);
        protected abstract string GetLocation(TEntity entity);
    }
}