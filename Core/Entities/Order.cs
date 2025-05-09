using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeGen.Core.TypeAnnotations;

namespace Core.Entities
{
    [ExportTsClass]
    public class Order
    {
        public int Id { get; set; }

        public int? CustomerId { get; set; }
        public User? Customer { get; set; }

        public int? CarId { get; set; }
        public Car? Car { get; set; }

        public int? BranchFromId { get; set; }
        public Branch? BranchFrom { get; set; }

        public int? BranchToId { get; set; }
        public Branch? BranchTo { get; set; }

        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal? PriceTotal { get; set; }

        public int? DiscountId { get; set; }
        public Discount? Discount { get; set; }

        public string? Status { get; set; }
        public string? Notes { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
