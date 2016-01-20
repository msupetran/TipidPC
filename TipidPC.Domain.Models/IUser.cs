using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipidPC.Domain.Models
{
    public interface IUser
    {
        int Id { get; set; }
        string Email { get; set; }
        string UserName { get; set; }
        string PhoneNumber { get; set; }
    }
}
