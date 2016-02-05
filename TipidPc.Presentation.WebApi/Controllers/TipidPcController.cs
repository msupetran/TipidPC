using Common.Infrastructure.Data;
using Common.Infrastructure.Domain;
using Common.Infrastructure.Specification;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TipidPc.Infrastructure.Data;

namespace TipidPc.Presentation.WebApi.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public abstract class TipidPcController<TRepository, TEntity> : ApiController
        where TRepository : IRepository<TEntity>
        where TEntity : class
    {
        // Fields
        private TRepository _repository;
        private ITpcContext _context;
        private IUnitOfWork _uow;

        // Properties
        protected TRepository Repository
        {
            get { return _repository; }
            set { _repository = value; }
        }
        protected ITpcContext Context
        {
            get { return _context; }
        }
        protected IUnitOfWork UnitOfWork
        {
            get { return _uow; }
        }

        // Constructors
        protected TipidPcController()
        {
            _context = new TpcContext();
            _uow = new TpcUnitOfWork(_context);
        }

        // Methods
        public virtual IHttpActionResult Get(int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var allUsersFilter = new ExpressionSpecification<TEntity>(u => true);
                    var users = this.Repository.Select(allUsersFilter);
                    return Ok(users);
                }

                var user = this.Repository.Select(id);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                // log error here...
                return InternalServerError(ex);
            }
        }
        public virtual IHttpActionResult Post([FromBody]TEntity user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }

                // do further validation...
                this.Repository.Insert(user);
                this.UnitOfWork.Commit();

                var location = Path.Combine(this.RequestContext.Url.Request.RequestUri.ToString(), user.ID.ToString());
                return Created<TEntity>(location, user);
            }
            catch (Exception ex)
            {
                // log exception...
                return InternalServerError();
            }
        }
        public virtual IHttpActionResult Put([FromBody]TEntity user)
        {
            try
            {
                if (id <= 0 || user == null)
                {
                    return BadRequest();
                }

                // do further validation...
                this.Repository.Update(user);
                this.UnitOfWork.Commit();

                return Ok(user);
            }
            catch (Exception ex)
            {
                // log exception...
                return InternalServerError();
            }
        }
        public virtual IHttpActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }

                // do further validation...
                this.Repository.Delete(id);
                this.UnitOfWork.Commit();

                return Ok();
            }
            catch (Exception ex)
            {
                // log exception...
                return InternalServerError();
            }
        }
    }
}