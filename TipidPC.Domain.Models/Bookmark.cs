using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipidPC.Domain.Models
{
    public class Bookmark
    {
        public int Id { get; set; }
        public int HeaderID { get; set; }
        public int UserID { get; set; }
        public DateTime Created { get; set; }

        // Navigation properties
        public virtual Header Header { get; set; }
        public virtual IUser User { get; set; }
    }
}
