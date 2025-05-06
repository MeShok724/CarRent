using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Maintenance
    {
        public int Id { get; set; }

        public int? CarId { get; set; }
        public Car? Car { get; set; }

        public string? Type { get; set; }
        public string? Description { get; set; }

        public decimal? Cost { get; set; }
        public DateTime? Date { get; set; }
    }
}
