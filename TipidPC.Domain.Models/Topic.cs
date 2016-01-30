using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipidPc.Domain.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public int HeaderId { get; set; }
        public int EntryId { get; set; }
        public int SectionId { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        // Navigation properties
        public virtual Header Header { get; set; }
        public virtual Entry Entry { get; set; }
        public virtual Section Section { get; set; }
        public virtual IUser User { get; set; }
    }
}
