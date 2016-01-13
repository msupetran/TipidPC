using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipidPC.Domain.Models
{
    public class Header
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserID { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
