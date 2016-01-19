using System;
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
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Inserting new item...");

                var context = new DbContext();
                var item = new Item()
                {
                    HeaderID = 1,
                    CategoryID = 1,
                    UserID = 1,
                    Amount = 275,
                    Section = ItemSection.ForSale,
                    Condition = ItemCondition.BrandNew,
                    Warranty = ItemWarranty.Personal,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    Expiry = DateTime.Now.AddDays(30)
                };
                var topic = new Topic()
                {
                    HeaderID = 1,
                    SectionID = 1,
                    UserID = 1,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                };
                
                using (var uow = new UnitOfWork(context))
                {
                    // Insert item...
                    var itemRepository = uow.GetRepository<Item>();
                    itemRepository.Insert(item);

                    var topicRepository = uow.GetRepository<Topic>();
                    topicRepository.Insert(topic);

                    uow.Commit();
                }

                Console.WriteLine("Item successfully inserted with ID No. {0}", item.Id);
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
    }
}
