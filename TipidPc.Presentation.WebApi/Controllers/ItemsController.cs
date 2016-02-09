using Common.Infrastructure.Data;
using Common.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TipidPc.Domain;
using TipidPc.Domain.Models;
using TipidPc.Infrastructure.Data;
using TipidPc.Infrastructure.Domain;
using TipidPc.Presentation.WebApi.Models;

namespace TipidPc.Presentation.WebApi.Controllers
{
    public class ItemsController : GenericApiController<Item, ItemDomainService, ItemsViewModel>
    {
        // Constructors
        public ItemsController()
        {
            _context = new TpcContext("Office");
            _uow = new TpcUnitOfWork(_context);
            _domainService = new ItemDomainService(_uow);
        }

        // Methods
        protected override IEnumerable<ItemsViewModel> GetViewModels(IEnumerable<Item> entities)
        {
            return entities.Select(i => new ItemsViewModel()
            {
                Id = i.Id,
                UserId = i.UserId,
                Section = i.Section,
                Name = i.Header.Title,
                CategoryId = i.CategoryId,
                Category = i.Category.Name,
                Amount = i.Amount,
                Condition = i.Condition,
                Warranty = i.Warranty,
                Duration = i.Duration,
                Description = i.Entry.Message
            });
        }
        protected override Item GetEntity(ItemsViewModel viewModel)
        {
            return new Item()
            {
                Header = new Header() { Title = viewModel.Name },
                Entry = new Entry() { Message = viewModel.Description },
                CategoryId = viewModel.CategoryId,
                Amount = viewModel.Amount,
                Condition = viewModel.Condition,
                Duration = viewModel.Duration,
                Section = viewModel.Section,
                Warranty = viewModel.Warranty,
                UserId = viewModel.UserId
            };
        }
        protected override string GetLocation(Item entity)
        {
            var uri = Path.Combine(this.RequestContext.Url.Request.RequestUri.ToString(), entity.Id.ToString());
            return uri;
        }

        protected override ItemsViewModel GetViewModel(Item i)
        {
            return new ItemsViewModel()
            {
                Id = i.Id,
                UserId = i.UserId,
                Section = i.Section,
                Name = i.Header.Title,
                CategoryId = i.CategoryId,
                Category = i.Category.Name,
                Amount = i.Amount,
                Condition = i.Condition,
                Warranty = i.Warranty,
                Duration = i.Duration,
                Description = i.Entry.Message
            };

        }
    }
}
