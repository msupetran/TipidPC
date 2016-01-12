using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipidPC.Domain.Models
{
    public enum MessageType : int
    {
        Negotiation = 0,
        Forum = 1
    }

    public class Message
    {
        public int ID { get; set; }
        public string Contents { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int UserID { get; set; }
    }
}
