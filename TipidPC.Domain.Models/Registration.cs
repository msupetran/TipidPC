using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipidPC.Domain.Models
{
    public class Registration
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public bool IsActivated { get; set; }
        public Guid ValidationID { get; set; }
        public DateTime Created { get; set; }
        public DateTime Activated { get; set; }
    }
}
