using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeGen.Core.TypeAnnotations;

namespace Core.Entities
{
    [ExportTsClass]
    public class Insurance
    {
        public int Id { get; set; }

        public int? CarId { get; set; }
        public Car? Car { get; set; }

        public string? PolicyNumber { get; set; }
        public string? CompanyName { get; set; }
        public string? InsuranceType { get; set; }

        public DateTime? IssueDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public string? FileUrl { get; set; }
    }
}
