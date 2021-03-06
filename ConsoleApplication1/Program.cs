﻿using Common.Infrastructure.Domain;
using Common.Infrastructure.Specification;
using System;
using System.Linq;
using System.Collections.Generic;
using TipidPc.Domain.Models;
using TipidPc.Infrastructure.Data;

namespace ConsoleApplication1
{
    class Program
    {
        // Fields
        private static ITpcContext _context;

        // Properties
        public static ITpcContext Context
        {
            get
            {
                if (_context == null)
                {
                    _context = new ApplicationDbContext();
                }
                return _context;
            }
        }
        public static DateTime Timestamp
        {
            get { return DateTime.Now; }
        }

        // Main
        static void Main(string[] args)
        {
            try
            {
                var items = GetItems();
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        Console.WriteLine("Name: {0}", item.Header.Title);
                        //Console.WriteLine("Description: {0}", item.Entry.Message);
                        Console.WriteLine("Category: {0}", item.Category.Name);
                        Console.WriteLine("Section: {0}", item.Section);
                        Console.WriteLine("Price: {0}", item.Amount);
                        Console.WriteLine("======================================");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to retrieve item.");
                }

                /*
                var insertedRecords = InsertItem();
                if (insertedRecords > 0)
                {
                    Console.WriteLine("Item successfully inserted {0} record(s).", insertedRecords);
                }
                else
                {
                    Console.WriteLine("Insert failed.");
                }
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message); 
                }
                Console.WriteLine(ex.GetType().ToString());
            }
            finally
            {
                Console.WriteLine("Press any key to continue...");
                Console.Read();
            }
        }

        // Methods
        static List<Item> GetItems()
        {
            using (var uow = new TpcUnitOfWork(Context))
            {
                // Insert item...
                var itemRepository = uow.GetRepository<Item>();

                var spec = new ExpressionSpecification<Item>()
                    .Not(i => (i.Header.Title.Contains("H")))
                    .Or(i => i.Amount >= 300);

                var include = new PropertyNavigator<Item>()
                    .Include(i => i.Header)
                    .Include(i => i.Entry)
                    .Properties;

                return itemRepository
                    .Select(spec, include)
                    .ToList();
            }
        }
        static int InsertCategory()
        {
            Console.WriteLine("Inserting category...");
            
            var category = new Category()
            {
                Name = "Processors",
                Created = Timestamp,
                Updated = Timestamp
            };

            using (var uow = new TpcUnitOfWork(Context))
            {
                var categoryRepository = uow.GetRepository<Category>();
                categoryRepository.Insert(category);
                return uow.Commit();
            }
        }
        static int InsertItem()
        {
            Console.WriteLine("Inserting item...");
            
            var header = new Header()
            {
                Title = string.Empty.PadRight(50, 'X'),
                UserId = 1,
                Created = Timestamp,
                Updated = Timestamp
            };
            var entry = new Entry()
            {
                Message = string.Empty.PadRight(2000, 'Y'),
                Created = Timestamp,
                Updated = Timestamp
            };
            var item = new Item()
            {
                Header = header,
                Entry = entry,
                CategoryId = 2,
                UserId = 1,
                Amount = 300,
                Section = ItemSection.ForSale,
                Condition = ItemCondition.BrandNew,
                Warranty = ItemWarranty.Personal,
                Created = Timestamp,
                Updated = Timestamp,
                Expiry = Timestamp.AddDays(30)
            };

            using (var uow = new TpcUnitOfWork(Context))
            {
                // Insert item...
                var itemRepository = uow.GetRepository<Item>();
                itemRepository.Insert(item);
                return uow.Commit();
            }
        }
    }

    class ApplicationDbContext : TpcContext
    {
        public ApplicationDbContext() : base("Office")
        {
        }
    }
}