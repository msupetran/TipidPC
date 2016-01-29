using System;
using Common.Infrastructure.Domain;
using Common.Infrastructure.Data;
using TipidPC.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TipidPC.Application
{
    public class ItemService
    {
        // Fields
        private IRepository<Header> _headerRepository;
        private IRepository<Item> _itemRepository;
        private IRepository<Entry> _entryRepository;
        
        // Constructors
        public ItemService(
            IRepository<Header> headerRespository, 
            IRepository<Item> itemRepository, 
            IRepository<Entry> entryRepository)
        {
            _headerRepository = headerRespository;
            _itemRepository = itemRepository;
            _entryRepository = entryRepository;
        }

        // Methods
        public void PostItem(
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

            if (categoryId <= 0)
            {
                throw new ValidationException("Category is invalid.");
            }

            if (amount <= 0)
            {
                throw new ValidationException("Amount is invalid");
            }

            if (userId <= 0)
            {
                throw new ValidationException("User is invalid.");
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
            if (header.Id <= 0)
            {
                throw new ApplicationException(@"Failure inserting to table ""Header"".");
            }

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

            // Commit - let the caller of this method handle the transaction...
            //var inserted = _unitOfWork.Commit();
            //return inserted;
        }
    }
}