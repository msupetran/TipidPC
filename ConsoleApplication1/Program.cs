﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TipidPC.Domain.Models;
using TipidPC.Infrastructure.Persistence;

namespace ConsoleApplication1
{
    class Program
    {
        // Properties
        public static ITpcContext Context
        {
            get { return new ApplicationDbContext(); }
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
                var insertedRecords = InsertItem();
                if (insertedRecords > 0)
                {
                    Console.WriteLine("Item successfully inserted {0} record(s).", insertedRecords);
                }
                else
                {
                    Console.WriteLine("Insert failed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to continue...");
                Console.Read();
            }
        }

        // Methods
        static int InsertCategory()
        {
            Console.WriteLine("Inserting category...");
            
            var category = new Category()
            {
                Name = "Hard Disk Drives",
                Created = Timestamp,
                Updated = Timestamp
            };

            using (var uow = new UnitOfWork(Context))
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
                Title = string.Empty.PadRight(50, 'H'),
                UserId = 1,
                Created = Timestamp,
                Updated = Timestamp
            };
            var entry = new Entry()
            {
                Message = string.Empty.PadRight(2000, 'M'),
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

            using (var uow = new UnitOfWork(Context))
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
        public ApplicationDbContext()
        {
        }
    }
}
