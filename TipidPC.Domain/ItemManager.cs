using System;
using Common.Infrastructure.Persistence;
using TipidPC.Domain.Models;
using Common.Infrastructure.Specification;

namespace TipidPC.Domain
{
    public class ItemManager
    {
        // Fields
        private IRepository<Header> _headerRepository;
        private IRepository<Entry> _entryRepository;
        private IRepository<Item> _itemRepository;
        private IRepository<IUser> _userRepository;
        private IRepository<Category> _categoryRepository;

        // Constructors
        public ItemManager(
            IRepository<IUser> userRepository, 
            IRepository<Category> categoryRepository, 
            IRepository<Header> headerRepository, 
            IRepository<Entry> entryRepository, 
            IRepository<Item> itemRepository)
        {
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _headerRepository = headerRepository;
            _entryRepository = entryRepository;
            _itemRepository = itemRepository;
        }

        // Methods
        public void InsertItem(Header header, Entry entry, Item item)
        {
            // Check if user exists
            if (_userRepository.Select(item.UserId) == null)
            {
                throw new ApplicationException("User is unkown.");
            }

            // Check if category exists
            if (_categoryRepository.Select(item.CategoryId) != null)
            {
                throw new ApplicationException("Could not find the specified category.");
            }

            // Validate other validity of fields here...
            // to do: add validation code

            // Insert header
            _headerRepository.Insert(header);

            // Insert entry
            _entryRepository.Insert(entry);

            // Insert item
            item.HeaderId = header.Id;
            _itemRepository.Insert(item);
        }
    }
}