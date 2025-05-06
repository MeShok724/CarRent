using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Branch
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public string? Phone { get; set; }

        public int? ManagerId { get; set; } 
        public User? Manager { get; set; }
    }
}
