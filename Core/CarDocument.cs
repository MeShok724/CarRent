using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class CarDocument
    {
        public int Id { get; set; }

        public int? CarId { get; set; }
        public Car? Car { get; set; }

        public string? Type { get; set; }
        public string? Number { get; set; }

        public DateTime? IssueDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public string? FileUrl { get; set; }
    }
}
