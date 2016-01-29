using System;
using Common.Infrastructure.Domain;
using TipidPC.Domain.Models;
using Common.Infrastructure.Specification;

namespace TipidPC.Domain
{
    public class ItemManager
    {
        // Fieldsy;
        private IRepository<Item> _itemRepository;

        // Constructors
        public ItemManager(IRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        // Methods
        public void InsertItem(Item item)
        {
            // Validate other fields here...
            // to do: add validation code
            if (string.IsNullOrEmpty(item.Header.Title.Trim()))
            {
                throw new ApplicationException("Header.Title is blank.");
            }

            // Insert item
            _itemRepository.Insert(item);
        }
    }
}