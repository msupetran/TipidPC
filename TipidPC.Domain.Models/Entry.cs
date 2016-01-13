using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipidPC.Domain.Models
{
    public class Entry
    {
        public int ID { get; set; }
        public int HeaderID { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
