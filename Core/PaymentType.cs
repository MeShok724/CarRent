using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class PaymentType
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
