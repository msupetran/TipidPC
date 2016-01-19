using System;
using Common.Infrastructure.Persistence;
using TipidPC.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TipidPC.Domain
{
    public class ItemManager
    {
        // Fields
        //private IUnitOfWork _unitOfWork;
        private IRepository<Header> _headerRepository;
        private IRepository<Item> _itemRepository;
        private IRepository<Entry> _entryRepository;
        
        // Constructors
        //public ItemManager(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}
        public ItemManager(
            IRepository<Header> headerRespository, 
            IRepository<Item> itemRepository, 
            IRepository<Entry> entryRepository)
        {
            _headerRepository = headerRespository;
            _itemRepository = itemRepository;
            _entryRepository = entryRepository;
        }

        // Methods
        public int Post(
            string name,
            string description,
            ItemSection section,
            int categoryId,
            int amount,
            ItemCondition condition,
            ItemWarranty warranty,
            ItemDuration duration,
            int userId)
        {
            // Check if parameters are valid...
            if (string.IsNullOrEmpty(name.Trim()) | name.Trim().Length > 50)
            {
                throw new ValidationException("Item name is either blank or has exceeded 50 characters.");
            }

            if (string.IsNullOrEmpty(description.Trim()) | description.Trim().Length > 500)
            {
                throw new ValidationException("Description is either blank or has exceeded 500 characters.");
            }

            if (categoryId <= 0 ||
                amount <= 0 ||
                userId <= 0)
            {
                return -1;
            }

            // Get date and time stamps...
            var timeStamp = DateTime.Now;

            // Insert header...
            var header = _headerRepository.Insert(
                new Header()
                {
                    Title = name,
                    UserId = userId,
                    Created = timeStamp,
                    Updated = timeStamp
                });

            // Insert item...
            var item = _itemRepository.Insert(
                new Item()
                {
                    HeaderId = header.Id,
                    CategoryId = categoryId,
                    UserId = userId,
                    Amount = amount,
                    Section = section,
                    Condition = condition,
                    Warranty = warranty,
                    Created = timeStamp,
                    Updated = timeStamp,
                    Expiry = timeStamp.AddDays((int)duration)
                });

            // Insert entry...
            var entry = _entryRepository.Insert(
                new Entry()
            {
                Message = description,
                HeaderId = header.Id,
                UserId = userId,
                Created = timeStamp,
                Updated = timeStamp
            });

            // Commit
            var inserted = _unitOfWork.Commit();
            return inserted;
        }
    }
}