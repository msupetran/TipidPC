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

                var i = 0;
                var context = new DbContext();
                var item = new Item()
                {
                    HeaderId = 1,
                    CategoryId = 1,
                    UserId = 1,
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

                    i = uow.Commit();
                }

                if (i > 0)
                {
                    Console.WriteLine("Item successfully inserted {0} records.", i);
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
    }
}
