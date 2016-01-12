using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipidPC.Domain.Models
{
    public enum ItemSection : int
    {
        ItemForSale = 0,
        WantToBuy = 1
    }

    public enum ItemCondition : int
    {
        BrandNew = 0,
        Used = 1,
        NewlyReplaced = 2,
        Defective = 3
    }

    public enum ItemWarranty : int
    {
        None = 0,
        Personal = 1,
        Shop = 2
    }

    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public decimal Amount { get; set; }
        public ItemSection Section { get; set; }
        public ItemCondition Condition { get; set; }
        public ItemWarranty Warranty { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Expiry { get; set; }
    }
}
