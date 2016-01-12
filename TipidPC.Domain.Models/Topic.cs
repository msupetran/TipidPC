using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipidPC.Domain.Models
{
    public class Topic
    {
        public int ID { get; set; }
        public string Subject { get; set; }
        public int MessageID { get; set; }
        public int SectionID { get; set; }
        public int UserID { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
