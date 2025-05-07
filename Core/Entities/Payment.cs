using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        public int? OrderId { get; set; }
        public Order? Order { get; set; }

        public int? CustomerId { get; set; }
        public User? Customer { get; set; }

        public decimal? Amount { get; set; }

        public int? PaymentTypeId { get; set; }
        public PaymentType? PaymentType { get; set; }

        public DateTime? PaymentDate { get; set; }
    }
}
