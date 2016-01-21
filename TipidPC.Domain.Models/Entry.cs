using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipidPC.Domain.Models
{
    public class Entry
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(2000)]
        public string Message { get; set; }
        public int HeaderId { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        // Navigation properties
        public Header Header { get; set; }
        public IUser User { get; set; }
    }
}
