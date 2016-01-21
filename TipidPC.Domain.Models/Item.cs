using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipidPC.Domain.Models
{
    public enum ItemSection : int
    {
        ForSale = 0,
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

    public enum ItemDuration : int
    {
        FifteenDays = 15,
        ThirtyDays = 30
    }

    public class Item
    {
        public int Id { get; set; }
        public int HeaderId { get; set; }
        public int EntryId { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public ItemSection Section { get; set; }
        [Required]
        public ItemCondition Condition { get; set; }
        [Required]
        public ItemWarranty Warranty { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        [Required]
        public DateTime Expiry { get; set; }

        // Navigation properties
        public Header Header { get; set; }
        public Entry Entry { get; set; }
        public Category Category { get; set; }
        public IUser User { get; set; }
    }
}
