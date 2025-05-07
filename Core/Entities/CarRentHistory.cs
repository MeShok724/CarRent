using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CarRentHistory
    {
        public int Id { get; set; }

        public int? CarId { get; set; }
        public Car? Car { get; set; }

        public int? CustomerId { get; set; }
        public User? Customer { get; set; }

        public int? OrderId { get; set; }
        public Order? Order { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
