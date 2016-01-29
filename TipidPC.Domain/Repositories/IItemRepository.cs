using Common.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TipidPC.Domain.Models;

namespace TipidPC.Domain
{
    public interface IItemRepository : 
        IRepository<Item>
    {
    }
}
