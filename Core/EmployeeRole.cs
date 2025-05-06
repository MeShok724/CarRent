using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class EmployeeRole
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
