﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipidPc.Domain.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public bool IsPositive { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        // Navigation properties
        public virtual IUser User { get; set; }
    }
}
