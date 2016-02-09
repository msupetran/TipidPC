using Common.Infrastructure.Domain;
using System;
using System.Collections.Generic;
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
    public class ItemsController : ApiController
    {
        // Fields
        private ItemDomainService _itemDomainService;

        // Constructors
        public ItemsController()
        {
            var itemRepository = new TpcRepository<Item>(new TpcContext());
            _itemDomainService = new ItemDomainService(itemRepository);
        }

        // Methods
        public IHttpActionResult Get(int id = 0)
        {
            try
            {
                if (id <= 0)
                {
                    var items = _itemDomainService.QueryItems();
                    var itemsViewModels = new List<ItemsViewModel>();
                    foreach (var i in items)
                    {
                        itemsViewModels.Add(new ItemsViewModel()
                        {
                            ID = i.Id,
                            Section = i.Section,
                            Name = i.Header.Title,
                            Category = i.Category,
                            Amount = i.Amount,
                            Condition = i.Condition,
                            Warranty = i.Warranty,
                            Duration = i.Duration,
                            Description = i.Entry.Message
                        });
                    }
                    if (itemsViewModels != null && itemsViewModels.Count() > 0)
                    {
                        return Ok(itemsViewModels); 
                    }
                }

                var item = _itemDomainService.QueryItemById(id);
                if (item != null)
                {
                    return Ok(item);
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
    }
}
