using Common.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TipidPc.Domain.Models;

namespace TipidPc.Domain
{
    public interface ITopicRepository : IRepository<Topic>
    {
    }
}
