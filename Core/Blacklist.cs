using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Blacklist
    {
        public int Id { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }

        public string? Reason { get; set; }

        public DateTime? BunnedAt { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
