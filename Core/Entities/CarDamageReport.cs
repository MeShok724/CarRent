using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeGen.Core.TypeAnnotations;

namespace Core.Entities
{
    [ExportTsClass]
    public class CarDamageReport
    {
        public int Id { get; set; }

        public int? CarId { get; set; }
        public Car? Car { get; set; }

        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public DateTime? ReportDate { get; set; }

        public string? Description { get; set; }
        public string? DamageType { get; set; }
        public string? PhotoUrl { get; set; }

        public bool? IsResolved { get; set; }
        public DateTime? ResolvedAt { get; set; }
    }
}
