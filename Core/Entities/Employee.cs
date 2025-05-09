using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeGen.Core.TypeAnnotations;

namespace Core.Entities
{
    [ExportTsClass]
    public class Employee
    {
        public int Id { get; set; }

        public int? RoleId { get; set; }
        public EmployeeRole? Role { get; set; }

        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }

        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
