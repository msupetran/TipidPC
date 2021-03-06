﻿using System;
using Common.Infrastructure.Domain;
using TipidPc.Domain.Models;
using Common.Infrastructure.Specification;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace TipidPc.Domain
{
    public class ItemDomainService
    {
        // Fields;
        private IRepository<Item> _itemRepository;
        List<ValidationResult> _validationErrors = new List<ValidationResult>();
        Expression<Func<Item, object>>[] _includedProperties = new PropertyNavigator<Item>()
            .Include(i => i.Header)
            .Include(i => i.Entry)
            .Properties;

        // Properties
        public List<ValidationResult> ValidationErrors
        {
            get { return _validationErrors; }
        }

        // Constructors
        public ItemDomainService(IRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        // Methods
        public Item QueryItemById(int id)
        {
            var itemIdSpec = new ExpressionSpecification<Item>(i => i.Id == id);
            var items = _itemRepository
                .Select(itemIdSpec, _includedProperties)
                .SingleOrDefault();
            return items;
        }
        public IEnumerable<Item> QueryItems()
        {
            var allSpec = new ExpressionSpecification<Item>();
            var items = _itemRepository.Select(allSpec, _includedProperties);
            return items;
        }
        public IEnumerable<Item> QueryItemsByUserId(int userId)
        {
            var itemUserIdSpec = new ExpressionSpecification<Item>(i => i.UserId == userId);
            var items = _itemRepository.Select(itemUserIdSpec, _includedProperties);
            return items;
        }
        public void AddItem(Item item)
        {
            var currentDate = DateTime.Now;
            item.Id = 0; 
            item.Created = currentDate;
            item.Updated = currentDate;
            item.Expiry = currentDate.AddDays((double)item.Duration);

            if (item.Header != null)
            {
                item.Header.Created = currentDate;
                item.Header.Created = currentDate;
            }

            if (item.Entry != null)
            {
                item.Entry.Created = currentDate;
                item.Entry.Updated = currentDate;
            }
            
            // Insert item
            if (Validate(item).Count() == 0)
            {
                _itemRepository.Insert(item);
            }
        }
        public void UpdateItem(Item newItem)
        {
            var currentDate = DateTime.Now;
            newItem.Updated = currentDate;

            if (this.Validate(newItem).Count() == 0)
            {
                _itemRepository.Update(newItem,
                    a => a.HeaderId,
                    a => a.EntryId,
                    a => a.UserId,
                    a => a.Created, 
                    a => a.Expiry);
            }
        }
        private List<ValidationResult> Validate(Item item)
        {
            // Validate item
            var itemValidator = new EntityValidator<Item>();
            _validationErrors.AddRange(itemValidator.GetValidationErrors(item));

            // Validate header
            if (item.Header != null)
            {
                var headerValidator = new EntityValidator<Header>();
                _validationErrors.AddRange(headerValidator.GetValidationErrors(item.Header));
            }

            // Validate entry
            if (item.Entry != null)
            {
                var entryValidator = new EntityValidator<Entry>();
                _validationErrors.AddRange(entryValidator.GetValidationErrors(item.Entry));
            }

            return _validationErrors;
        }
    }
}