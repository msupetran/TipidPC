using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TipidPc.Domain.Models;

namespace TipidPc.Presentation.WebApi.Models
{
    public class ItemsViewModel
    {
        public int ID { get; set; }
        public ItemSection Section { get; set; }
        public string Name { get; set; }
        public Category Category{ get; set; }
        public decimal Amount { get; set; }
        public ItemCondition Condition { get; set; }
        public ItemWarranty Warranty { get; set; }
        public ItemDuration Duration { get; set; }
        public string Description { get; set; }
    }
}